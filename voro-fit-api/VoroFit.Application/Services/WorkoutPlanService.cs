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
    public class WorkoutPlanService(IWorkoutPlanRepository exerciseRepository, IMapper mapper) : ServiceBase<WorkoutPlan>(exerciseRepository), IWorkoutPlanService
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
            return await exerciseRepository.Query()
                .Include(s => s.Student)
                .Include(s => s.Weeks)
                .ProjectTo<WorkoutPlanDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<WorkoutPlanDto?> GetByIdAsync(Guid id)
        {
            return await exerciseRepository.Query()
                .Where(s => s.Id == id)
                .ProjectTo<WorkoutPlanDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
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
