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
    public class WorkoutPlanWeekService(IWorkoutPlanWeekRepository exerciseRepository, IMapper mapper) : ServiceBase<WorkoutPlanWeek>(exerciseRepository), IWorkoutPlanWeekService
    {
        public async Task<WorkoutPlanWeekDto> CreateAsync(WorkoutPlanWeekDto dto)
        {
            var createWorkoutPlanWeekDto = mapper.Map<WorkoutPlanWeek>(dto);

            var exercise = base.AddAsync(createWorkoutPlanWeekDto);

            return mapper.Map<WorkoutPlanWeekDto>(exercise);
        }

        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<WorkoutPlanWeekDto>> GetAllAsync()
        {
            return await exerciseRepository.Query()
                .Include(s => s.Days)
                .Include(s => s.WorkoutPlan)
                .ProjectTo<WorkoutPlanWeekDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<WorkoutPlanWeekDto?> GetByIdAsync(Guid id)
        {
            return await exerciseRepository.Query()
                .Where(s => s.Id == id)
                .ProjectTo<WorkoutPlanWeekDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<WorkoutPlanWeekDto> UpdateAsync(Guid id, WorkoutPlanWeekDto dto)
        {
            var updateWorkoutPlanWeekDto = mapper.Map<WorkoutPlanWeek>(dto);

            var existingWorkoutPlanWeek = base.GetByIdAsync(id);

            if (existingWorkoutPlanWeek != null)
            {
                base.Update(updateWorkoutPlanWeekDto);
            }

            return mapper.Map<WorkoutPlanWeekDto>(updateWorkoutPlanWeekDto);
        }
    }
}
