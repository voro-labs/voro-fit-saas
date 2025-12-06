using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoroFit.API.Extensions;
using VoroFit.API.ViewModels;
using VoroFit.Application.DTOs.Evolution.API;
using VoroFit.Application.Services.Interfaces.Evolution;

namespace VoroFit.API.Controllers.Evolution
{
    [Route("api/v{version:version}/[controller]")]
    [Tags("Evolution")]
    [ApiController]
    [Authorize]
    public class InstanceController(IEvolutionService evolutionService) : ControllerBase
    {
        private readonly IEvolutionService _evolutionService = evolutionService;

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var status = await _evolutionService.GetInstanceStatusAsync();

                return ResponseViewModel<InstanceEventDto>
                    .SuccessWithMessage("Status successful.", status)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<InstanceEventDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }
    }
}
