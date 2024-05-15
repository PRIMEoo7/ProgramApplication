using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgramApplicationApi.Interfaces;
using ProgramApplicationApi.Models;

namespace ProgramApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramRepository _programRepository;

        public ProgramController(IProgramRepository programRepository)
        {
            _programRepository = programRepository;
        }

        [HttpPost("/CreateProgram")]
        public async Task<IActionResult> CreateProgramInfo(ProgramFieldDefinition programInfo)
        {
            try
            {
                await _programRepository.CreateProgram(programInfo);

                return Ok("Program created successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut("{id}/UpdateProgram")]
        public async Task<IActionResult> UpdateProgramInfo(string Id, string programTitle, ProgramFieldDefinition programInfo)
        {
            try
            {
                await _programRepository.UpdateProgram(Id, programTitle, programInfo);

                return Ok("Program Update successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
