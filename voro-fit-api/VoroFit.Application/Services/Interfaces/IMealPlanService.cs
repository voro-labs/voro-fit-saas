using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IMealPlanService : IServiceBase<MealPlan>
    {
        Task<IEnumerable<MealPlanDto>> GetAllAsync();
        Task<MealPlanDto?> GetByIdAsync(Guid id);
        Task<MealPlanDto> CreateAsync(MealPlanDto model);
        Task<MealPlanDto> UpdateAsync(Guid id, MealPlanDto model);
        Task DeleteAsync(Guid id);
    }
}
