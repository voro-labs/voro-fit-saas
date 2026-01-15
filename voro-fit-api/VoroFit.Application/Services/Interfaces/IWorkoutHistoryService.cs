using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IWorkoutHistoryService : IServiceBase<WorkoutHistory>
    {
        Task<IEnumerable<WorkoutHistoryDto>> GetAllAsync();
        Task<WorkoutHistoryDto?> GetByIdAsync(Guid id);
        Task<WorkoutHistoryDto> CreateAsync(WorkoutHistoryDto model);
        Task<WorkoutHistoryDto> UpdateAsync(Guid id, WorkoutHistoryDto model);
        Task DeleteAsync(Guid id);
    }
}
