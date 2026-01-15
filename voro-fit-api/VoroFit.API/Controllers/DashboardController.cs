using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoroFit.Shared.Extensions;
using VoroFit.Shared.ViewModels;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Interfaces;

namespace VoroFit.API.Controllers
{
    [Route("api/v{version:version}/[controller]")]
    [Tags("Dashboard")]
    [ApiController]
    [Authorize]
    public class DashboardController(IDashboardService dashboardService) : ControllerBase
    {
        // ----------------------------------------------------
        // GET /dashboard
        // ----------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await dashboardService.GetAllAsync();

                return ResponseViewModel<DashboardDataDto>
                    .Success(result)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<DashboardDataDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }
    }
}
