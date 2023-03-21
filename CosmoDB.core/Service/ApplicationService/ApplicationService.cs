using AutoMapper;
using Azure;
using CosmoDB.core.Exceptions;
using CosmoDB.core.Models.DTOs;
using CosmoDB.core.Models.viewModels;
using CosmoDB.core.Repositories.BlobRepository;
using CosmoDB.core.Repositories.CosmosRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Service.ApplicationService
{
    public class ApplicationService : IApplicationService
    {
        private readonly IMapper _iMapper;
        private readonly IBlobRepository _blobRepository;
        private readonly ICosmosRepository _cosmosRepository;
        public ApplicationService(IBlobRepository blobRepository, ICosmosRepository cosmosRepository, IMapper imapper)
        {
            _blobRepository = blobRepository;
            _cosmosRepository = cosmosRepository;
            _iMapper = imapper;
        }

        public async Task<ApplicationFormDTO> GetApplicationFormDetails(string programId)
        {
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(programId);
            if (storedProgramDetails.Form == null) throw new ErrorOccuredShortCircuitException($"No Application form exists for this program");
            return storedProgramDetails.Form;
        }

        public async Task<FullProgramDetails> UploadCoverImage(UploadImageModel data)
        {

            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(data.ProgramId);
            if (storedProgramDetails?.Form?.Image?.FileName != null) throw new RequestFailedException($"Program already has an image. Please delete current image before uploading.");
            var uploadedImage = await _blobRepository.UploadApplicationImage(data);
            if(storedProgramDetails == null) storedProgramDetails = new FullProgramDetails();
            storedProgramDetails.Form = storedProgramDetails.Form ?? new ApplicationFormDTO();
            storedProgramDetails.Form.Image = uploadedImage;
            return await _cosmosRepository.Update(storedProgramDetails);
        }

        public async Task<FullProgramDetails?> DeleteCoverImage(string programId)
        {
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(programId);
            if (storedProgramDetails.Form?.Image?.FileName == null) throw new ErrorOccuredShortCircuitException($"Image not found.");
            var isDeleteSuccess = await _blobRepository.DeleteApplicationImage(storedProgramDetails.Form.Image.FileName);
            if (isDeleteSuccess)
            {
                storedProgramDetails.Form.Image = null;
                return await _cosmosRepository.Update(storedProgramDetails);
            }
            return null;
        }

        public async Task<FullProgramDetails> AddQuestion(UploadQuestionModel questionDetails)
        {
            var mappedQuestionDetails = _iMapper.Map<QuestionDTO>(questionDetails);
            mappedQuestionDetails.Id = Guid.NewGuid().ToString();
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(questionDetails.ProgramId);
            if(storedProgramDetails.Form == null) storedProgramDetails.Form = new ApplicationFormDTO();
            switch (questionDetails.section)
            {
                case QuestionSections.PersonalInformation:
                    storedProgramDetails.Form.PersonalInformationQuestions = AddQuestion(mappedQuestionDetails, storedProgramDetails.Form.PersonalInformationQuestions);
                    break;
                case QuestionSections.Profile:
                    storedProgramDetails.Form.ProfileQuestions = AddQuestion(mappedQuestionDetails, storedProgramDetails.Form.ProfileQuestions);
                    break;
                default:
                    storedProgramDetails.Form.AdditionalQuestions = AddQuestion(mappedQuestionDetails, storedProgramDetails.Form.AdditionalQuestions);
                    break;

            }

            return await _cosmosRepository.Update(storedProgramDetails);
        }
        public async Task<FullProgramDetails> RemoveQuestion(RemoveQuestionModel questionDetails)
        {
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(questionDetails.ProgramId);
            if (storedProgramDetails.Form == null) storedProgramDetails.Form = new ApplicationFormDTO();
            switch (questionDetails.Section)
            {
                case QuestionSections.PersonalInformation:
                    storedProgramDetails.Form.PersonalInformationQuestions = DeleteQuestion(questionDetails, storedProgramDetails.Form.PersonalInformationQuestions);
                    break;
                case QuestionSections.Profile:
                    storedProgramDetails.Form.ProfileQuestions = DeleteQuestion(questionDetails, storedProgramDetails.Form.ProfileQuestions);
                    break;
                default:
                    storedProgramDetails.Form.AdditionalQuestions = DeleteQuestion(questionDetails, storedProgramDetails.Form.AdditionalQuestions);
                    break;
            }

            return await _cosmosRepository.Update(storedProgramDetails);
        }

        public async Task<FullProgramDetails> UpdateQuestion(UpdateQuestionModel questionDetails)
        {
            var mappedQuestionDetails = _iMapper.Map<QuestionDTO>(questionDetails);
            var storedProgramDetails = await _cosmosRepository.GetSpecificProgram(questionDetails.ProgramId);
            if (storedProgramDetails.Form == null) storedProgramDetails.Form = new ApplicationFormDTO();
            switch (questionDetails.section)
            {
                case QuestionSections.PersonalInformation:
                    storedProgramDetails.Form.PersonalInformationQuestions = UpdateQuestion(mappedQuestionDetails, storedProgramDetails.Form.PersonalInformationQuestions);
                    break;
                case QuestionSections.Profile:
                    storedProgramDetails.Form.ProfileQuestions = UpdateQuestion(mappedQuestionDetails, storedProgramDetails.Form.ProfileQuestions);
                    break;
                default:
                    storedProgramDetails.Form.AdditionalQuestions = UpdateQuestion(mappedQuestionDetails, storedProgramDetails.Form.AdditionalQuestions);
                    break;
            }

            return await _cosmosRepository.Update(storedProgramDetails);
        }

        public List<QuestionDTO> AddQuestion(QuestionDTO question, List<QuestionDTO>? questions)
        {
            if(questions == null)questions = new List<QuestionDTO>();
            questions.Add(question);
            return questions;
        }

        public List<QuestionDTO> DeleteQuestion(RemoveQuestionModel questionDetails, List<QuestionDTO>? questions )
        {
            if (questions == null)throw new ErrorOccuredShortCircuitException($"Question not found in specified section");
            var question = questions.SingleOrDefault(x => x.Id == questionDetails.QuestionId);
            if (question == null) throw new ErrorOccuredShortCircuitException($"Question not found in specified section");
            questions.Remove(question);
            return questions;
        }

        public List<QuestionDTO> UpdateQuestion(QuestionDTO updatedQuestion, List<QuestionDTO>? questions)
        {
            if (questions == null) throw new ErrorOccuredShortCircuitException($"Question not found in specified section");
            var questionIndex = questions.FindIndex(x => x.Id == updatedQuestion.Id);
            if (questionIndex < 0) throw new ErrorOccuredShortCircuitException($"Question not found in specified section");
            questions[questionIndex] = updatedQuestion;
            return questions;
        }
    }
}
