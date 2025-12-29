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
    public class WorkoutPlanService(IWorkoutPlanRepository workoutPlanRepository, IMapper mapper) : ServiceBase<WorkoutPlan>(workoutPlanRepository), IWorkoutPlanService
    {
        public async Task<WorkoutPlanDto> CreateAsync(WorkoutPlanDto dto)
        {
            var createWorkoutPlanDto = mapper.Map<WorkoutPlan>(dto);

            var exercise = base.AddAsync(createWorkoutPlanDto);

            return mapper.Map<WorkoutPlanDto>(exercise);
        }

        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<WorkoutPlanDto>> GetAllAsync()
        {
            var workoutPlans = await base.Query()
                .Include(s => s.Student)
                    .ThenInclude(u => u.UserExtension)
                        .ThenInclude(u => u.User)
                .Include(w => w.Weeks)
                    .ThenInclude(w => w.Days)
                        .ThenInclude(w => w.Exercises)
                .ToListAsync();

            return mapper.Map<IEnumerable<WorkoutPlanDto>>(workoutPlans);
        }

        public async Task<WorkoutPlanDto?> GetByIdAsync(Guid id)
        {
            var workoutPlan = await base.Query()
                .Include(s => s.Student)
                    .ThenInclude(u => u.UserExtension)
                        .ThenInclude(u => u.User)
                .Include(w => w.Weeks)
                    .ThenInclude(w => w.Days)
                        .ThenInclude(w => w.Exercises)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            return mapper.Map<WorkoutPlanDto?>(workoutPlan);
        }

        public async Task<WorkoutPlanDto> UpdateAsync(Guid id, WorkoutPlanDto dto)
        {
            var updateWorkoutPlanDto = mapper.Map<WorkoutPlan>(dto);

            var existingWorkoutPlan = base.GetByIdAsync(id);

            if (existingWorkoutPlan != null)
            {
                base.Update(updateWorkoutPlanDto);
            }

            return mapper.Map<WorkoutPlanDto>(updateWorkoutPlanDto);
        }
    }
}
