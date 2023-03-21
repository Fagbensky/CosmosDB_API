using CosmoDB.core.Models.DTOs;
using CosmoDB.core.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Service.ProgramService
{
    public interface IProgramService
    {
        Task<FullProgramDetails> CreateProgram(ProgramModel details);
        Task<FullProgramDetails> GetSpecificProgram(string programId);
        Task<List<FullProgramDetails>> GetPrograms();
        Task<ProgramDTO> GetSpecificProgramDetail(string programId);
        Task<FullProgramDetails> UpdateProgramDetails(UpdateProgramModel details);
    }
}
