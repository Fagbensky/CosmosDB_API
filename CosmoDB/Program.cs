using CosmoDB.core.Configurations;
using CosmoDB.core.Repositories.CosmosRepository;
using CosmoDB.core.Filters;
using CosmoDB.core.Mapping;
using CosmoDB.core.Service.ProgramService;
using Microsoft.Azure.Cosmos;
using CosmoDB.core.Repositories.BlobRepository;
using CosmoDB.core.Service.ApplicationService;
using CosmoDB.core.Service.StageService;
using Serilog;
using Serilog.Events;
using Serilog.Core;
using CosmoDB.core.Logger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

if (builder.Environment.IsDevelopment())
{
    var logger = new LoggerConfiguration()
               .ReadFrom.Configuration(builder.Configuration)
               .Enrich.FromLogContext()
               .CreateLogger();


    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
    builder.Logging.AddConsole();
}
else
{
    var connectionString = builder.Configuration.GetSection("BlobConnectionSettings").Get<BlobConnectionSettings>().connectionString;
    Log.Logger = new LoggerConfiguration()
                .WriteTo.AzureBlobStorage(connectionString: connectionString,
                 restrictedToMinimumLevel: LogEventLevel.Debug,
                 storageFileName: "{yyyy}-{MM}-{dd}/WebLog.txt",
                 outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                 storageContainerName: "logs").CreateBootstrapLogger();
    builder.Host.UseSerilog();
}

builder.Services.AddSingleton<ICosmosRepository, CosmosRepository>(options =>
{
    var azureConfigs = builder.Configuration.GetSection("AzureCosmosDbSettings").Get<AzureCosmosDbSettings>();
    if(azureConfigs == null) azureConfigs = new AzureCosmosDbSettings();
    string url = azureConfigs.URL;
    string primaryKey = azureConfigs.PrimaryKey;
    string dbName = azureConfigs.DatabaseName;
    string containerName = azureConfigs.ContainerName;
    var cosmosClient = new CosmosClient(url, primaryKey);
    var cosmosContainer = cosmosClient.GetContainer(dbName, containerName);
    return new CosmosRepository(cosmosClient, cosmosContainer);
});
builder.Services.AddSingleton<IBlobRepository, BlobRepository>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IStageService, StageService>(); 
builder.Services.AddSingleton<LogWriter>();
builder.Services.Configure<BlobConnectionSettings>(builder.Configuration.GetSection("BlobConnectionSettings")); 
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
