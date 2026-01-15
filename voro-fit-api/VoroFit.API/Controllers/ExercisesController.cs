using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoroFit.Shared.Extensions;
using VoroFit.Shared.ViewModels;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Interfaces;

namespace VoroFit.API.Controllers
{
    [Route("api/v{version:version}/[controller]")]
    [Tags("Exercises")]
    [ApiController]
    [Authorize]
    public class ExercisesController(IExerciseService exerciseService) : ControllerBase
    {
        // ----------------------------------------------------
        // GET /exercises
        // ----------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var exercises = await exerciseService.GetAllAsync();

                return ResponseViewModel<IEnumerable<ExerciseDto>>
                    .Success(exercises)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<IEnumerable<ExerciseDto>>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ----------------------------------------------------
        // GET /exercises/{id}
        // ----------------------------------------------------
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var exercise = await exerciseService.GetByIdAsync(id);

                return ResponseViewModel<ExerciseDto>
                    .Success(exercise)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<ExerciseDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ----------------------------------------------------
        // POST /exercises
        // ----------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ExerciseDto model)
        {
            try
            {
                var created = await exerciseService.CreateAsync(model);

                await exerciseService.SaveChangesAsync();

                return ResponseViewModel<ExerciseDto>
                    .SuccessWithMessage("Exercício criado com sucesso.", created)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<ExerciseDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ----------------------------------------------------
        // PUT /exercises/{id}
        // ----------------------------------------------------
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ExerciseDto model)
        {
            try
            {
                var updated = await exerciseService.UpdateAsync(id, model);

                await exerciseService.SaveChangesAsync();

                return ResponseViewModel<ExerciseDto>
                    .SuccessWithMessage("Exercício atualizado com sucesso.", updated)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<ExerciseDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ----------------------------------------------------
        // DELETE /exercises/{id}
        // ----------------------------------------------------
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await exerciseService.DeleteAsync(id);

                await exerciseService.SaveChangesAsync();

                return ResponseViewModel<string>
                    .SuccessWithMessage("Exercício removido com sucesso.", null)
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