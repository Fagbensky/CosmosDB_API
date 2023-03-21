using Azure;
using Azure.Storage.Blobs.Models;
using CosmoDB.core.Exceptions;
using CosmoDB.core.Models.DTOs;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Repositories.CosmosRepository
{
    public class CosmosRepository : ICosmosRepository
    {
        private readonly Container _cosmosContainer;
        public CosmosRepository(CosmosClient cosmosClient, Container cosmosContainer)
        {
            _cosmosContainer = cosmosContainer;
        }

        public async Task<FullProgramDetails> CreateNewProgramAsync(FullProgramDetails programDetails)
        {
            var item = await _cosmosContainer.CreateItemAsync<FullProgramDetails>(programDetails, new PartitionKey(programDetails.id));
            return item;
        }

        public async Task<FullProgramDetails> GetSpecificProgram(string programId)
        {
            var response = new FullProgramDetails();
            try
            {
                var cosmoDbResponse = await _cosmosContainer.ReadItemAsync<FullProgramDetails>(programId, new PartitionKey(programId));
                response = cosmoDbResponse.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ErrorOccuredShortCircuitException($"No data found for {programId}.");
            }
            return response;
        }

        public async Task<List<FullProgramDetails>> GetPrograms()
        {
            var sqlQueryText = "SELECT * FROM c";

            var query = _cosmosContainer.GetItemQueryIterator<FullProgramDetails>(new QueryDefinition(sqlQueryText));

            List<FullProgramDetails> result = new List<FullProgramDetails>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response);
            }

            return result;
        }

        public async Task<FullProgramDetails> Update(FullProgramDetails programDetails)
        {
            var item = await _cosmosContainer.UpsertItemAsync<FullProgramDetails>(programDetails, new PartitionKey(programDetails.id));
            return item;
        }
    }
}
