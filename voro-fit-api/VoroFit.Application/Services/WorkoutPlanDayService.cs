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
    public class WorkoutPlanDayService(IWorkoutPlanDayRepository workoutPlanDayRepository, IMapper mapper) : ServiceBase<WorkoutPlanDay>(workoutPlanDayRepository), IWorkoutPlanDayService
    {
        public async Task<WorkoutPlanDayDto> CreateAsync(WorkoutPlanDayDto dto)
        {
            var createWorkoutPlanDayDto = mapper.Map<WorkoutPlanDay>(dto);

            var exercise = base.AddAsync(createWorkoutPlanDayDto);

            return mapper.Map<WorkoutPlanDayDto>(exercise);
        }

        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<WorkoutPlanDayDto>> GetAllAsync()
        {
            var workoutPlanDays = await base.Query()
                .Include(s => s.Exercises)
                .Include(s => s.WorkoutPlanWeek)
                .ToListAsync();

            return mapper.Map<IEnumerable<WorkoutPlanDayDto>>(workoutPlanDays);
        }

        public async Task<WorkoutPlanDayDto?> GetByIdAsync(Guid id)
        {
            var workoutPlanDay = await base.Query()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            return mapper.Map<WorkoutPlanDayDto?>(workoutPlanDay);
        }

        public async Task<WorkoutPlanDayDto> UpdateAsync(Guid id, WorkoutPlanDayDto dto)
        {
            var updateWorkoutPlanDayDto = mapper.Map<WorkoutPlanDay>(dto);

            var existingWorkoutPlanDay = base.GetByIdAsync(id);

            if (existingWorkoutPlanDay != null)
            {
                base.Update(updateWorkoutPlanDayDto);
            }

            return mapper.Map<WorkoutPlanDayDto>(updateWorkoutPlanDayDto);
        }
    }
}
