using CosmoDB.core.Models.DTOs;
using CosmoDB.core.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Service.ApplicationService
{
    public interface IApplicationService
    {
        Task<ApplicationFormDTO> GetApplicationFormDetails(string programId);
        Task<FullProgramDetails> UploadCoverImage(UploadImageModel data);
        Task<FullProgramDetails?> DeleteCoverImage(string programId);
        Task<FullProgramDetails> AddQuestion(UploadQuestionModel questionDetails);
        Task<FullProgramDetails> RemoveQuestion(RemoveQuestionModel questionDetails);
        Task<FullProgramDetails> UpdateQuestion(UpdateQuestionModel questionDetails);
    }
}
