using Microsoft.AspNetCore.Mvc;
using ProgramApplicationApi.Interfaces;
using ProgramApplicationApi.Models;
using ProgramApplicationApi.Repositories;

namespace ProgramApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationController(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        [HttpGet("{id}/{programTitle}/GetProgram")]
        public async Task<IActionResult> GetProgram([FromRoute] Guid id, string programTitle)
        {
            try
            {
                var results = await _applicationRepository.GetProgram(id, programTitle);

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("SubmitApplication")]
        public async Task<IActionResult> SubmitApplication(ApplicationModel applicationInfo)
        {
            try
            {
                await _applicationRepository.SubmitApplication(applicationInfo);

                return Ok("Program created successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
