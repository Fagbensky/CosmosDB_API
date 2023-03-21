using CosmoDB.core.Models.DTOs;
using CosmoDB.core.Models.viewModels;
using CosmoDB.core.ResponseStatus;
using CosmoDB.core.Service.ProgramService;
using Microsoft.AspNetCore.Mvc;

namespace CosmoDB.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProgramController : Controller
    {
        IProgramService _programService;
        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("CreateProgram")]
        public async Task<IActionResult> CreateProgram(ProgramModel programDetails)
        {
            var createdProgram = await _programService.CreateProgram(programDetails);
            return StatusCode(StatusCodes.Status201Created, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = createdProgram
            });
        }

        [ProducesResponseType(typeof(ReturnVM<List<FullProgramDetails>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("GetPrograms")]
        public async Task<IActionResult> getPrograms()
        {
            var programs = await _programService.GetPrograms();
            return StatusCode(StatusCodes.Status200OK, new ReturnVM<List<FullProgramDetails>>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = programs
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("GetProgram/{id}")]
        public async Task<IActionResult> getPrograms(string id)
        {
            var program = await _programService.GetSpecificProgram(id);
            return StatusCode(StatusCodes.Status200OK, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = program
            });
        }

        [ProducesResponseType(typeof(ReturnVM<ProgramDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("GetProgramDetails/{id}")]
        public async Task<IActionResult> getProgramsDetails(string id)
        {
            var programsDetails = await _programService.GetSpecificProgramDetail(id);
            return StatusCode(StatusCodes.Status200OK, new ReturnVM<ProgramDTO>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = programsDetails
            });
        }

        [ProducesResponseType(typeof(ReturnVM<FullProgramDetails>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReturnVM<string>), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("UpdateProgramDetails")]
        public async Task<IActionResult> UpdateProgramDetails(UpdateProgramModel programDetails)
        {
            var updatedProgramDetails = await _programService.UpdateProgramDetails(programDetails);
            return StatusCode(StatusCodes.Status200OK, new ReturnVM<FullProgramDetails>
            {
                ResponseCode = ResponseStatus.success.code,
                ResponseDescription = ResponseStatus.success.message,
                ResponseData = updatedProgramDetails
            });
        }
    }
}
