using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoroFit.API.Extensions;
using VoroFit.API.ViewModels;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Interfaces;

namespace VoroFit.API.Controllers
{
    [Route("api/v{version:version}/workout-plans")]
    [Tags("Workouts")]
    [ApiController]
    [Authorize]
    public class WorkoutPlansController(IWorkoutPlanService workoutService) : ControllerBase
    {
        private readonly IWorkoutPlanService _workoutService = workoutService;

        // ----------------------------------------------------
        // GET /workouts
        // ----------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var workouts = await _workoutService.GetAllAsync();

                return ResponseViewModel<IEnumerable<WorkoutPlanDto>>
                    .Success(workouts)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<IEnumerable<WorkoutPlanDto>>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ----------------------------------------------------
        // GET /workouts/{id}
        // ----------------------------------------------------
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var workout = await _workoutService.GetByIdAsync(id);

                return ResponseViewModel<WorkoutPlanDto>
                    .Success(workout)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<WorkoutPlanDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ----------------------------------------------------
        // POST /workouts
        // ----------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkoutPlanDto model)
        {
            try
            {
                var created = await _workoutService.CreateAsync(model);

                return ResponseViewModel<WorkoutPlanDto>
                    .SuccessWithMessage("Treino criado com sucesso.", created)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<WorkoutPlanDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ----------------------------------------------------
        // PUT /workouts/{id}
        // ----------------------------------------------------
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] WorkoutPlanDto model)
        {
            try
            {
                var updated = await _workoutService.UpdateAsync(id, model);

                return ResponseViewModel<WorkoutPlanDto>
                    .SuccessWithMessage("Treino atualizado com sucesso.", updated)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<WorkoutPlanDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ----------------------------------------------------
        // DELETE /workouts/{id}
        // ----------------------------------------------------
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _workoutService.DeleteAsync(id);

                return ResponseViewModel<string>
                    .SuccessWithMessage("Treino excluído com sucesso.", null)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<string>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }
    }
}
