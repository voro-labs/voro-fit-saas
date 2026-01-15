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
            var workoutPlanDay = mapper.Map<WorkoutPlanDay>(dto);

            await base.AddAsync(workoutPlanDay);

            return mapper.Map<WorkoutPlanDayDto>(workoutPlanDay);
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
            var existingWorkoutPlanDay = await base.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("WorkoutPlanDay não encontrado");
            
            mapper.Map(dto, existingWorkoutPlanDay);

            base.Update(existingWorkoutPlanDay);

            return mapper.Map<WorkoutPlanDayDto>(existingWorkoutPlanDay);
        }
    }
}
