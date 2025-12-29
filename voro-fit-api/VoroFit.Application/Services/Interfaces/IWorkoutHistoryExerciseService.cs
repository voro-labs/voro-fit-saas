using VoroFit.Domain.Entities;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Application.DTOs;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IWorkoutHistoryExerciseService : IServiceBase<WorkoutHistoryExercise>
    {
        Task<IEnumerable<WorkoutHistoryExerciseDto>> GetAllAsync();
        Task<WorkoutHistoryExerciseDto?> GetByIdAsync(Guid id);
        Task<WorkoutHistoryExerciseDto> CreateAsync(WorkoutHistoryExerciseDto model);
        Task<WorkoutHistoryExerciseDto> UpdateAsync(Guid id, WorkoutHistoryExerciseDto model);
        Task DeleteAsync(Guid id);
    }
}
