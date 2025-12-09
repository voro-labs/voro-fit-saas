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
    public class MealPlanService(IMealPlanRepository mealPlanRepository, IMapper mapper) : ServiceBase<MealPlan>(mealPlanRepository), IMealPlanService
    {
        public async Task<MealPlanDto> CreateAsync(MealPlanDto dto)
        {
            var createMealPlanDto = mapper.Map<MealPlan>(dto);

            var exercise = base.AddAsync(createMealPlanDto);

            return mapper.Map<MealPlanDto>(exercise);
        }

        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<MealPlanDto>> GetAllAsync()
        {
            return await mealPlanRepository.Query()
                .Include(s => s.Student)
                .Include(s => s.Days)
                    .ThenInclude(d => d.Meals)
                .Include(s => s.Days)
                    .ThenInclude(d => d.MealPlan)
                .ProjectTo<MealPlanDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<MealPlanDto?> GetByIdAsync(Guid id)
        {
            return await mealPlanRepository.Query()
                .Where(s => s.Id == id)
                .ProjectTo<MealPlanDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<MealPlanDto> UpdateAsync(Guid id, MealPlanDto dto)
        {
            var updateMealPlanDto = mapper.Map<MealPlan>(dto);

            var existingMealPlan = base.GetByIdAsync(id);

            if (existingMealPlan != null)
            {
                base.Update(updateMealPlanDto);
            }

            return mapper.Map<MealPlanDto>(updateMealPlanDto);
        }
    }
}
