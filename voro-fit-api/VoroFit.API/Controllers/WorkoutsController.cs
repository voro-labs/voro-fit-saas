using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoroFit.API.Extensions;
using VoroFit.API.ViewModels;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Interfaces;

namespace VoroFit.API.Controllers
{
    [Route("api/v{version:version}/[controller]")]
    [Tags("Workouts")]
    [ApiController]
    [Authorize]
    public class WorkoutsController(IWorkoutHistoryService workoutService) : ControllerBase
    {
        // ----------------------------------------------------
        // GET /workouts
        // ----------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var workouts = await workoutService.GetAllAsync();

                return ResponseViewModel<IEnumerable<WorkoutHistoryDto>>
                    .Success(workouts)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<IEnumerable<WorkoutHistoryDto>>
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
                var workout = await workoutService.GetByIdAsync(id);

                return ResponseViewModel<WorkoutHistoryDto>
                    .Success(workout)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<WorkoutHistoryDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ----------------------------------------------------
        // POST /workouts
        // ----------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkoutHistoryDto model)
        {
            try
            {
                var created = await workoutService.CreateAsync(model);

                await workoutService.SaveChangesAsync();

                return ResponseViewModel<WorkoutHistoryDto>
                    .SuccessWithMessage("Treino criado com sucesso.", created)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<WorkoutHistoryDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ----------------------------------------------------
        // PUT /workouts/{id}
        // ----------------------------------------------------
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] WorkoutHistoryDto model)
        {
            try
            {
                var updated = await workoutService.UpdateAsync(id, model);

                await workoutService.SaveChangesAsync();

                return ResponseViewModel<WorkoutHistoryDto>
                    .SuccessWithMessage("Treino atualizado com sucesso.", updated)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<WorkoutHistoryDto>
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
                await workoutService.DeleteAsync(id);

                await workoutService.SaveChangesAsync();

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
