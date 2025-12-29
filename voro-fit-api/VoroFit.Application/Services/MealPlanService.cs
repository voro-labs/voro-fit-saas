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
            var mealPlans = await base.Query()
                .Include(s => s.Student)
                    .ThenInclude(s => s.UserExtension)
                        .ThenInclude(s => s.User)
                .Include(s => s.Days)
                    .ThenInclude(d => d.Meals)
                .Include(s => s.Days)
                .ToListAsync();

            return mapper.Map<IEnumerable<MealPlanDto>>(mealPlans);
        }

        public async Task<MealPlanDto?> GetByIdAsync(Guid id)
        {
            var mealPlan = await base.Query()
                .Include(s => s.Student)
                    .ThenInclude(s => s.UserExtension)
                        .ThenInclude(s => s.User)
                .Include(s => s.Days)
                    .ThenInclude(d => d.Meals)
                .Include(s => s.Days)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            return mapper.Map<MealPlanDto?>(mealPlan);
        }

        public async Task<MealPlanDto> UpdateAsync(Guid id, MealPlanDto dto)
        {
            var existingMealPlan = await base.GetByIdAsync(id);
            
            mapper.Map(dto, existingMealPlan);

            if (existingMealPlan != null)
            {
                base.Update(existingMealPlan);
            }

            return mapper.Map<MealPlanDto>(existingMealPlan);
        }
    }
}
