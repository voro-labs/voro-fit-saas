using VoroFit.Domain.Entities;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Application.DTOs;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IWorkoutPlanWeekService : IServiceBase<WorkoutPlanWeek>
    {
        Task<IEnumerable<WorkoutPlanWeekDto>> GetAllAsync();
        Task<WorkoutPlanWeekDto?> GetByIdAsync(Guid id);
        Task<WorkoutPlanWeekDto> CreateAsync(WorkoutPlanWeekDto model);
        Task<WorkoutPlanWeekDto> UpdateAsync(Guid id, WorkoutPlanWeekDto model);
        Task DeleteAsync(Guid id);
    }
}
