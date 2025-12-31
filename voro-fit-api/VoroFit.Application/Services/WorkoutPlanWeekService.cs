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
    public class WorkoutPlanWeekService(IWorkoutPlanWeekRepository workoutPlanWeekRepository, IMapper mapper) : ServiceBase<WorkoutPlanWeek>(workoutPlanWeekRepository), IWorkoutPlanWeekService
    {
        public async Task<WorkoutPlanWeekDto> CreateAsync(WorkoutPlanWeekDto dto)
        {
            var createWorkoutPlanWeekDto = mapper.Map<WorkoutPlanWeek>(dto);

            await base.AddAsync(createWorkoutPlanWeekDto);

            return mapper.Map<WorkoutPlanWeekDto>(createWorkoutPlanWeekDto);
        }

        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<WorkoutPlanWeekDto>> GetAllAsync()
        {
            var workoutPlanWeeks = await base.Query()
                .Include(s => s.Days)
                .Include(s => s.WorkoutPlan)
                .ToListAsync();

            return mapper.Map<IEnumerable<WorkoutPlanWeekDto>>(workoutPlanWeeks);
        }

        public async Task<WorkoutPlanWeekDto?> GetByIdAsync(Guid id)
        {
            var workoutPlanWeek = await base.Query()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            return mapper.Map<WorkoutPlanWeekDto?>(workoutPlanWeek);
        }

        public async Task<WorkoutPlanWeekDto> UpdateAsync(Guid id, WorkoutPlanWeekDto dto)
        {
            var existingWorkoutPlanWeek = await base.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("WorkoutPlanWeek não encontrado");
            
            mapper.Map(dto, existingWorkoutPlanWeek);

            base.Update(existingWorkoutPlanWeek);

            return mapper.Map<WorkoutPlanWeekDto>(existingWorkoutPlanWeek);
        }
    }
}
