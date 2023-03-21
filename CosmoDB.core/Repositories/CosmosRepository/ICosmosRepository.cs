using CosmoDB.core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Repositories.CosmosRepository
{
    public interface ICosmosRepository
    {
        Task<FullProgramDetails> CreateNewProgramAsync(FullProgramDetails programDetails);
        Task<List<FullProgramDetails>> GetPrograms();
        Task<FullProgramDetails> GetSpecificProgram(string programId);
        Task<FullProgramDetails> Update(FullProgramDetails programDetails);
    }
}
