using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Configurations
{
    public class AzureCosmosDbSettings
    {
        public string URL { get; set; } = "";
        public string PrimaryKey { get; set; } = "";
        public string DatabaseName { get; set; } = "";
        public string ContainerName { get; set; } = "";
    }
}
