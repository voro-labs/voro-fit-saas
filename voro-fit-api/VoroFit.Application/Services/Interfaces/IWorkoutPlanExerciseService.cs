using VoroFit.Domain.Entities;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Application.DTOs;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IWorkoutPlanExerciseService : IServiceBase<WorkoutPlanExercise>
    {
        Task<IEnumerable<WorkoutPlanExerciseDto>> GetAllAsync();
        Task<WorkoutPlanExerciseDto?> GetByIdAsync(Guid id);
        Task<WorkoutPlanExerciseDto> CreateAsync(WorkoutPlanExerciseDto model);
        Task<WorkoutPlanExerciseDto> UpdateAsync(Guid id, WorkoutPlanExerciseDto model);
        Task DeleteAsync(Guid id);
    }
}
