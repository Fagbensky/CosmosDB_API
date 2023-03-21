using AutoMapper;
using CosmoDB.core.Models.viewModels;
using CosmoDB.core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmoDB.core.Repositories.CosmosRepository;
using CosmoDB.core.Exceptions;

namespace CosmoDB.core.Service.ProgramService
{
    public class ProgramService: IProgramService
    {
        readonly IMapper _iMapper;
        readonly ICosmosRepository _cosmosRepository;
        public ProgramService(IMapper imapper, ICosmosRepository cosmosRepository) 
        { 
            _iMapper = imapper;
            _cosmosRepository = cosmosRepository;
        }

        public async Task<FullProgramDetails> CreateProgram(ProgramModel details) 
        {
            var newProgram = new FullProgramDetails
            {
                id = Guid.NewGuid().ToString(),
                Details = _iMapper.Map<ProgramDTO>(details)
            };
            return await _cosmosRepository.CreateNewProgramAsync(newProgram);
        }

        public async Task<FullProgramDetails> GetSpecificProgram(string programId)
        {
            return await _cosmosRepository.GetSpecificProgram(programId);
        }

        public async Task<List<FullProgramDetails>> GetPrograms()
        {
            return await _cosmosRepository.GetPrograms();
        }

        public async Task<ProgramDTO> GetSpecificProgramDetail(string programId)
        {
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(programId);
            if (storedProgramDetails.Details == null) throw new ErrorOccuredShortCircuitException($"Program doesn't exist");
            return storedProgramDetails.Details;
        }

        public async Task<FullProgramDetails> UpdateProgramDetails(UpdateProgramModel details)
        {
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(details.programId);
            storedProgramDetails.Details = _iMapper.Map<ProgramDTO>(details);
            return await _cosmosRepository.Update(storedProgramDetails);
        }
    }
}
