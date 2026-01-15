using VoroFit.Domain.Entities;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Application.DTOs;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IWorkoutPlanService : IServiceBase<WorkoutPlan>
    {
        Task<IEnumerable<WorkoutPlanDto>> GetAllAsync();
        Task<WorkoutPlanDto?> GetByIdAsync(Guid id);
        Task<WorkoutPlanDto> CreateAsync(WorkoutPlanDto model);
        Task<WorkoutPlanDto> UpdateAsync(Guid id, WorkoutPlanDto model);
        Task DeleteAsync(Guid id);
    }

}
