using CosmoDB.core.Models.DTOs;
using CosmoDB.core.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Service.StageService
{
    public interface IStageService
    {
        Task<List<StageDTO>> GetStage(string programId);
        Task<FullProgramDetails> CreateStage(UploadStageModel details);
        Task<FullProgramDetails> UpdateStage(UpdateStageModel details);
        Task<FullProgramDetails> DeleteStage(RemoveStageModel details);
        Task<FullProgramDetails> CreateVideoQuestion(UploadVideoQuestionsModel details);
        Task<FullProgramDetails> UpdateVideoQuestion(UpdateVideoQuestionModel details);
        Task<FullProgramDetails> DeleteVideoQuestion(RemoveVideoQuestionModel details);
    }
}
