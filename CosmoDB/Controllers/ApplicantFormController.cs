using CosmoDB.core.Models.DTOs;
using CosmoDB.core.Models.viewModels;
using CosmoDB.core.Repositories.BlobRepository;
using CosmoDB.core.Filters;
using CosmoDB.core.ResponseStatus;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CosmoDB.core.Service.ApplicationService;

namespace CosmoDB.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApplicantFormController : Controller
    {
        private readonly IApplicationService _applicationService;
        public ApplicantFormController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [ProducesResponseType(typeof(ReturnVM<ApplicationFormDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("GetApplicationFormDetails/{id}")]
        public async Task<IActionResult> UploadCoverImage(string id)
        {
            var applicationFormDetails = await _applicationService.GetApplicationFormDetails(id);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<ApplicationFormDTO>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = applicationFormDetails
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [MultipartFormDataFilter]
        [HttpPost]
        [Route("UploadCoverImage")]
        public async Task<IActionResult> UploadCoverImage([FromForm] UploadImageModel image)
        {
            var updatedProgram = await _applicationService.UploadCoverImage(image);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = updatedProgram
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("DeleteCoverImage/{id}")]
        public async Task<IActionResult> DeleteCoverImage(string id)
        {
            var updatedProgram = await _applicationService.DeleteCoverImage(id);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = updatedProgram
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("AddQuestion")]
        public async Task<IActionResult> AddQuestion(UploadQuestionModel question)
        {
            var updatedProgram = await _applicationService.AddQuestion(question);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = updatedProgram
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("RemoveQuestion")]
        public async Task<IActionResult> AddQuestion(RemoveQuestionModel questionDetails)
        {
            var updatedProgram = await _applicationService.RemoveQuestion(questionDetails);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = updatedProgram
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestion(UpdateQuestionModel questionDetails)
        {
            var updatedProgram = await _applicationService.UpdateQuestion(questionDetails);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = updatedProgram
            });
        }
    }
}
