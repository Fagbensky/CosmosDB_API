using CosmoDB.core.Models.DTOs;
using CosmoDB.core.Models.viewModels;
using CosmoDB.core.ResponseStatus;
using CosmoDB.core.Service.ApplicationService;
using CosmoDB.core.Service.StageService;
using Microsoft.AspNetCore.Mvc;

namespace CosmoDB.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class StageController : Controller
    {
        private readonly IStageService _stageService;
        public StageController(IStageService stageService)
        {
            _stageService= stageService;
        }

        [ProducesResponseType(typeof(ReturnVM<List<StageDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("GetStages/{id}")]
        public async Task<IActionResult> CreateStage(string id)
        {
            var stageDetails = await _stageService.GetStage(id);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<List<StageDTO>>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = stageDetails
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("CreateStage")]
        public async Task<IActionResult> CreateStage(UploadStageModel stageDetails)
        {
            var updatedProgramDetails= await _stageService.CreateStage(stageDetails);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = updatedProgramDetails
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("UpdateStage")]
        public async Task<IActionResult> UpdateStage(UpdateStageModel stageDetails)
        {
            var updatedProgramDetails = await _stageService.UpdateStage(stageDetails);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = updatedProgramDetails
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("RemoveStage")]
        public async Task<IActionResult> RemoveStage(RemoveStageModel stageDetails)
        {
            var updatedProgramDetails = await _stageService.DeleteStage(stageDetails);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = updatedProgramDetails
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("CreateStageVideoQuestion")]
        public async Task<IActionResult> CreateVideoQuestions(UploadVideoQuestionsModel videoQuestionDetails)
        {
            var updatedProgramDetails = await _stageService.CreateVideoQuestion(videoQuestionDetails);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = updatedProgramDetails
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("UpdateStageVideoQuestion")]
        public async Task<IActionResult> UpdateStageVideoQuestion(UpdateVideoQuestionModel videoQuestionDetails)
        {
            var updatedProgramDetails = await _stageService.UpdateVideoQuestion(videoQuestionDetails);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = updatedProgramDetails
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("RemoveStageVideoQuestion")]
        public async Task<IActionResult> RemoveStageVideoQuestion(RemoveVideoQuestionModel videoQuestionDetails)
        {
            var updatedProgramDetails = await _stageService.DeleteVideoQuestion(videoQuestionDetails);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = updatedProgramDetails
            });
        }
    }
}
