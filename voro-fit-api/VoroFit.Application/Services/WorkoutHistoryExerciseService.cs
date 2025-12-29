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
    public class WorkoutHistoryExerciseService(IWorkoutHistoryExerciseRepository workoutHistoryExerciseRepository, IMapper mapper) : ServiceBase<WorkoutHistoryExercise>(workoutHistoryExerciseRepository), IWorkoutHistoryExerciseService
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
            var workoutHistoryExercises = await base.Query()
                .Include(s => s.Exercise)
                .Include(s => s.WorkoutHistory)
                .ToListAsync();

            return mapper.Map<IEnumerable<WorkoutHistoryExerciseDto>>(workoutHistoryExercises);
        }

        public async Task<WorkoutHistoryExerciseDto?> GetByIdAsync(Guid id)
        {
            var workoutHistoryExercise = await base.Query()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            return mapper.Map<WorkoutHistoryExerciseDto?>(workoutHistoryExercise);
        }

        public async Task<WorkoutHistoryExerciseDto> UpdateAsync(Guid id, WorkoutHistoryExerciseDto dto)
        {
            var existingWorkoutHistoryExercise = await base.GetByIdAsync(id);
            
            mapper.Map(dto, existingWorkoutHistoryExercise);

            if (existingWorkoutHistoryExercise != null)
            {
                base.Update(existingWorkoutHistoryExercise);
            }

            return mapper.Map<WorkoutHistoryExerciseDto>(existingWorkoutHistoryExercise);
        }
    }
}
