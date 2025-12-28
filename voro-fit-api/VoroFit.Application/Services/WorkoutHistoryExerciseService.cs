using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Domain.Entities;
using VoroFit.Domain.Interfaces.Repositories;

namespace VoroFit.Application.Services
{
    public class WorkoutHistoryExerciseService(IWorkoutHistoryExerciseRepository exerciseRepository, IMapper mapper) : ServiceBase<WorkoutHistoryExercise>(exerciseRepository), IWorkoutHistoryExerciseService
    {
        public async Task<WorkoutHistoryExerciseDto> CreateAsync(WorkoutHistoryExerciseDto dto)
        {
            var createWorkoutHistoryExerciseDto = mapper.Map<WorkoutHistoryExercise>(dto);

            var exercise = base.AddAsync(createWorkoutHistoryExerciseDto);

            return mapper.Map<WorkoutHistoryExerciseDto>(exercise);
        }

        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<WorkoutHistoryExerciseDto>> GetAllAsync()
        {
            return await exerciseRepository.Query()
                .Include(s => s.Exercise)
                .Include(s => s.WorkoutHistory)
                .ProjectTo<WorkoutHistoryExerciseDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<WorkoutHistoryExerciseDto?> GetByIdAsync(Guid id)
        {
            return await exerciseRepository.Query()
                .Where(s => s.Id == id)
                .ProjectTo<WorkoutHistoryExerciseDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<WorkoutHistoryExerciseDto> UpdateAsync(Guid id, WorkoutHistoryExerciseDto dto)
        {
            var updateWorkoutHistoryExerciseDto = mapper.Map<WorkoutHistoryExercise>(dto);

            var existingWorkoutHistoryExercise = base.GetByIdAsync(id);

            if (existingWorkoutHistoryExercise != null)
            {
                base.Update(updateWorkoutHistoryExerciseDto);
            }

            return mapper.Map<WorkoutHistoryExerciseDto>(updateWorkoutHistoryExerciseDto);
        }
    }
}
