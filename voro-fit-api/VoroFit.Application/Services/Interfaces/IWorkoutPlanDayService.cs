using VoroFit.Domain.Entities;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Application.DTOs;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IWorkoutPlanDayService : IServiceBase<WorkoutPlanDay>
    {
        Task<IEnumerable<WorkoutPlanDayDto>> GetAllAsync();
        Task<WorkoutPlanDayDto?> GetByIdAsync(Guid id);
        Task<WorkoutPlanDayDto> CreateAsync(WorkoutPlanDayDto model);
        Task<WorkoutPlanDayDto> UpdateAsync(Guid id, WorkoutPlanDayDto model);
        Task DeleteAsync(Guid id);
    }
}
