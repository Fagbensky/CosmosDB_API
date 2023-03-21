using AutoMapper;
using CosmoDB.core.Exceptions;
using CosmoDB.core.Models.DTOs;
using CosmoDB.core.Models.viewModels;
using CosmoDB.core.Repositories.CosmosRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Service.StageService
{
    public class StageService: IStageService
    {
        readonly IMapper _iMapper;
        readonly ICosmosRepository _cosmosRepository;
        public StageService(IMapper imapper, ICosmosRepository cosmosRepository)
        {
            _iMapper = imapper;
            _cosmosRepository = cosmosRepository;
        }

        public async Task<List<StageDTO>> GetStage(string programId)
        {
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(programId);
            if(storedProgramDetails.Stages == null) throw new ErrorOccuredShortCircuitException($"No Stages currently exist for this program");
            return storedProgramDetails.Stages;
        }

        public async Task<FullProgramDetails> CreateStage(UploadStageModel details)
        {
            var mappedStageDetails = _iMapper.Map<StageDTO>(details);
            mappedStageDetails.Id = Guid.NewGuid().ToString();
            if (mappedStageDetails.VideoQuestions != null)
            {
                for (int i = 0; i < mappedStageDetails.VideoQuestions.Count; i++)
                {
                    mappedStageDetails.VideoQuestions[i].Id = Guid.NewGuid().ToString();
                }
            }
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(details.ProgramId);
            if (storedProgramDetails.Stages == null) storedProgramDetails.Stages = new List<StageDTO>();
            storedProgramDetails.Stages.Add(mappedStageDetails);
            return await _cosmosRepository.Update(storedProgramDetails);
        }

        public async Task<FullProgramDetails> UpdateStage(UpdateStageModel details)
        {
            var mappedStageDetails = _iMapper.Map<StageDTO>(details);
            if (mappedStageDetails.VideoQuestions != null)
            {
                for (int i = 0; i < mappedStageDetails.VideoQuestions.Count; i++)
                {
                    mappedStageDetails.VideoQuestions[i].Id = Guid.NewGuid().ToString();
                }
            }
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(details.ProgramId);
            if (storedProgramDetails.Stages == null) throw new ErrorOccuredShortCircuitException($"Stage not found");
            var stageIndex = storedProgramDetails.Stages.FindIndex(x => x.Id == mappedStageDetails.Id);
            if (stageIndex < 0) throw new ErrorOccuredShortCircuitException($"Stage not found");
            storedProgramDetails.Stages[stageIndex] = mappedStageDetails;
            return await _cosmosRepository.Update(storedProgramDetails);
        }
        public async Task<FullProgramDetails> DeleteStage(RemoveStageModel details)
        {
            var mappedStageDetails = _iMapper.Map<StageDTO>(details);
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(details.ProgramId);
            if (storedProgramDetails.Stages == null) throw new ErrorOccuredShortCircuitException($"Stage not found");
            var stage = storedProgramDetails.Stages.FirstOrDefault(x => x.Id == mappedStageDetails.Id);
            if (stage == null) throw new ErrorOccuredShortCircuitException($"Stage not found");
            storedProgramDetails.Stages.Remove( mappedStageDetails);
            return await _cosmosRepository.Update(storedProgramDetails);
        }

        public async Task<FullProgramDetails> CreateVideoQuestion(UploadVideoQuestionsModel details)
        {
            var mappedVideoQuestion = _iMapper.Map<VideoQuestionDTO>(details);
            mappedVideoQuestion.Id = Guid.NewGuid().ToString();
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(details.ProgramId);
            if (storedProgramDetails.Stages == null) throw new ErrorOccuredShortCircuitException($"Stage not found");
            var stageIndex = storedProgramDetails.Stages.FindIndex(x => x.Id == details.StageId);
            if (stageIndex < 0) throw new ErrorOccuredShortCircuitException($"Stage not found");
            if (storedProgramDetails.Stages[stageIndex].VideoQuestions == null) storedProgramDetails.Stages[stageIndex].VideoQuestions = new List<VideoQuestionDTO>();
            storedProgramDetails.Stages[stageIndex].VideoQuestions.Add(mappedVideoQuestion);
            return await _cosmosRepository.Update(storedProgramDetails);
        }

        public async Task<FullProgramDetails> UpdateVideoQuestion(UpdateVideoQuestionModel details)
        {
            var mappedVideoQuestion = _iMapper.Map<VideoQuestionDTO>(details);
            mappedVideoQuestion.Id = Guid.NewGuid().ToString();
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(details.ProgramId);
            if (storedProgramDetails.Stages == null) throw new ErrorOccuredShortCircuitException($"Stage not found");
            var stageIndex = storedProgramDetails.Stages.FindIndex(x => x.Id == details.StageId);
            if (stageIndex < 0) throw new ErrorOccuredShortCircuitException($"Stage not found");
            if (storedProgramDetails.Stages[stageIndex].VideoQuestions == null) throw new ErrorOccuredShortCircuitException($"Video question not found");
            var videoQuestionIndex = storedProgramDetails.Stages[stageIndex].VideoQuestions.FindIndex(x => x.Id == details.VideoQuestionId);
            if (videoQuestionIndex < 0) throw new ErrorOccuredShortCircuitException($"Video question not found");
            storedProgramDetails.Stages[stageIndex].VideoQuestions[videoQuestionIndex] = mappedVideoQuestion;
            return await _cosmosRepository.Update(storedProgramDetails);
        }

        public async Task<FullProgramDetails> DeleteVideoQuestion(RemoveVideoQuestionModel details)
        {
            var mappedVideoQuestion = _iMapper.Map<VideoQuestionDTO>(details);
            mappedVideoQuestion.Id = Guid.NewGuid().ToString();
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(details.ProgramId);
            if (storedProgramDetails.Stages == null) throw new ErrorOccuredShortCircuitException($"Stage not found");
            var stageIndex = storedProgramDetails.Stages.FindIndex(x => x.Id == details.StageId);
            if (stageIndex < 0) throw new ErrorOccuredShortCircuitException($"Stage not found");
            if (storedProgramDetails.Stages[stageIndex].VideoQuestions == null) throw new ErrorOccuredShortCircuitException($"Video not found");
            var videoQuestion = storedProgramDetails.Stages[stageIndex].VideoQuestions.FirstOrDefault(x => x.Id == details.VideoQuestionId);
            if (videoQuestion == null) throw new ErrorOccuredShortCircuitException($"Video not found");
            storedProgramDetails.Stages[stageIndex].VideoQuestions.Remove(videoQuestion);
            return await _cosmosRepository.Update(storedProgramDetails);
        }
    }
}
