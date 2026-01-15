using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IExerciseService : IServiceBase<Exercise>
    {
        Task<IEnumerable<ExerciseDto>> GetAllAsync();
        Task<ExerciseDto?> GetByIdAsync(Guid id);
        Task<ExerciseDto> CreateAsync(ExerciseDto model);
        Task<ExerciseDto> UpdateAsync(Guid id, ExerciseDto model);
        Task DeleteAsync(Guid id);
    }
}
