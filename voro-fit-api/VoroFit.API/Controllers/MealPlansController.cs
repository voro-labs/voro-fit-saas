using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoroFit.API.Extensions;
using VoroFit.API.ViewModels;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Interfaces;

namespace VoroFit.API.Controllers
{
    [Route("api/v{version:version}/meal-plans")]
    [Tags("MealPlans")]
    [ApiController]
    [Authorize]
    public class MealPlansController(IMealPlanService mealPlanService) : ControllerBase
    {
        private readonly IMealPlanService _mealPlanService = mealPlanService;

        // GET /api/v1/mealplans
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _mealPlanService.GetAllAsync();

                return ResponseViewModel<IEnumerable<MealPlanDto>>
                    .SuccessWithMessage("Meal plans loaded successfully.", result)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<IEnumerable<MealPlanDto>>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // GET /api/v1/mealplans/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _mealPlanService.GetByIdAsync(id);

                if (result is null)
                {
                    return ResponseViewModel<MealPlanDto>
                        .Fail("Meal plan not found.")
                        .ToActionResult();
                }

                return ResponseViewModel<MealPlanDto>
                    .SuccessWithMessage("Meal plan loaded successfully.", result)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<MealPlanDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // POST /api/v1/mealplans
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MealPlanDto dto)
        {
            try
            {
                var result = await _mealPlanService.CreateAsync(dto);

                return ResponseViewModel<MealPlanDto>
                    .SuccessWithMessage("Meal plan created successfully.", result)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<MealPlanDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // PUT /api/v1/mealplans/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MealPlanDto dto)
        {
            try
            {
                var result = await _mealPlanService.UpdateAsync(id, dto);

                if (result is null)
                {
                    return ResponseViewModel<MealPlanDto>
                        .Fail("Meal plan not found.")
                        .ToActionResult();
                }

                return ResponseViewModel<MealPlanDto>
                    .SuccessWithMessage("Meal plan updated successfully.", result)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<MealPlanDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // DELETE /api/v1/mealplans/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _mealPlanService.DeleteAsync(id);

                return ResponseViewModel<object>
                    .SuccessWithMessage("Meal plan deleted successfully.", true)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<object>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }
    }
}
