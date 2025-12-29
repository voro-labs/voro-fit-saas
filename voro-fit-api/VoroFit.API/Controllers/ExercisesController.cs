using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoroFit.API.Extensions;
using VoroFit.API.ViewModels;
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
        private readonly IExerciseService _exerciseService = exerciseService;

        // ----------------------------------------------------
        // GET /exercises
        // ----------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var exercises = await _exerciseService.GetAllAsync();

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
                var exercise = await _exerciseService.GetByIdAsync(id);

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
        // POST /exercises (FormData)
        // ----------------------------------------------------
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ExerciseDto model)
        {
            try
            {
                var created = await _exerciseService.CreateAsync(model);

                await _exerciseService.SaveChangesAsync();

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
        // PUT /exercises/{id} (FormData)
        // ----------------------------------------------------
        [HttpPut("{id:guid}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(Guid id, [FromForm] ExerciseDto model)
        {
            try
            {
                var updated = await _exerciseService.UpdateAsync(id, model);

                await _exerciseService.SaveChangesAsync();

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
                await _exerciseService.DeleteAsync(id);

                await _exerciseService.SaveChangesAsync();

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