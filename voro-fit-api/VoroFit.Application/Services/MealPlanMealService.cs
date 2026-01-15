using VoroFit.Domain.Entities;
using VoroFit.Application.Services.Base;
using VoroFit.Domain.Interfaces.Repositories;
using VoroFit.Application.Services.Interfaces;

namespace VoroFit.Application.Services
{
    public class MealPlanMealService(IMealPlanMealRepository mealPlanMealRepository) : ServiceBase<MealPlanMeal>(mealPlanMealRepository), IMealPlanMealService
    {
    }
}
