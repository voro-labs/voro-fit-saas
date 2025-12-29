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
    public class WorkoutPlanExerciseService(IWorkoutPlanExerciseRepository workoutPlanExerciseRepository, IMapper mapper) : ServiceBase<WorkoutPlanExercise>(workoutPlanExerciseRepository), IWorkoutPlanExerciseService
    {
        public async Task<WorkoutPlanExerciseDto> CreateAsync(WorkoutPlanExerciseDto dto)
        {
            var createWorkoutPlanExerciseDto = mapper.Map<WorkoutPlanExercise>(dto);

            var exercise = base.AddAsync(createWorkoutPlanExerciseDto);

            return mapper.Map<WorkoutPlanExerciseDto>(exercise);
        }

        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<WorkoutPlanExerciseDto>> GetAllAsync()
        {
            var workoutPlanExercises = await base.Query()
                .Include(s => s.Exercise)
                .Include(s => s.WorkoutPlanDay)
                .ToListAsync();

            return mapper.Map<IEnumerable<WorkoutPlanExerciseDto>>(workoutPlanExercises);
        }

        public async Task<WorkoutPlanExerciseDto?> GetByIdAsync(Guid id)
        {
            var workoutPlanExercise = await base.Query()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            return mapper.Map<WorkoutPlanExerciseDto?>(workoutPlanExercise);
        }

        public async Task<WorkoutPlanExerciseDto> UpdateAsync(Guid id, WorkoutPlanExerciseDto dto)
        {
            var updateWorkoutPlanExerciseDto = mapper.Map<WorkoutPlanExercise>(dto);

            var existingWorkoutPlanExercise = base.GetByIdAsync(id);

            if (existingWorkoutPlanExercise != null)
            {
                base.Update(updateWorkoutPlanExerciseDto);
            }

            return mapper.Map<WorkoutPlanExerciseDto>(updateWorkoutPlanExerciseDto);
        }
    }
}
