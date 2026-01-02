using System.Text.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Domain.Entities;
using VoroFit.Domain.Interfaces.Repositories;
using VoroFit.Shared.Helpers;

namespace VoroFit.Application.Services
{
    public class MealPlanService(IMealPlanRepository mealPlanRepository, IMapper mapper) : ServiceBase<MealPlan>(mealPlanRepository), IMealPlanService
    {
        public async Task<MealPlanDto> CreateAsync(MealPlanDto dto)
        {
            var createMealPlanDto = mapper.Map<MealPlan>(dto);

            await base.AddAsync(createMealPlanDto);

            return mapper.Map<MealPlanDto>(createMealPlanDto);
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
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            return mapper.Map<MealPlanDto?>(mealPlan);
        }

        public async Task<MealPlanDto> UpdateAsync(Guid id, MealPlanDto dto)
        {
            var existingMealPlan = await base.GetByIdAsync(
                mp => mp.Id == id,
                mp => mp.Include(x => x.Days)
                        .ThenInclude(d => d.Meals)
            ) ?? throw new Exception("MealPlan não encontrado");

            mapper.Map(dto, existingMealPlan);

            SyncDays(existingMealPlan, dto);

            existingMealPlan.UpdatedAt = DateTimeOffset.UtcNow;

            base.Update(existingMealPlan);

            return mapper.Map<MealPlanDto>(existingMealPlan);
        }

        private static void SyncDays(MealPlan plan, MealPlanDto dto)
        {
            CollectionSyncHelper.Sync(
                plan.Days,
                dto.Days ?? [],
                db => db.Id,
                d => d.Id ?? Guid.Empty,
                d =>
                {
                    var day = new MealPlanDay
                    {
                        DayOfWeek = d.DayOfWeek ?? Domain.Enums.DayOfWeekEnum.Sunday,
                        MealPlanId = plan.Id,
                        CreatedAt = DateTimeOffset.UtcNow
                    };

                    SyncMeals(day, d);

                    return day;
                },
                (db, d) =>
                {
                    db.DayOfWeek = d.DayOfWeek ?? Domain.Enums.DayOfWeekEnum.Sunday;
                    db.UpdatedAt = DateTimeOffset.UtcNow;
                    SyncMeals(db, d);
                },
                db =>
                {
                    db.IsDeleted = true;
                    db.DeletedAt = DateTimeOffset.UtcNow;
                }
            );
        }


        private static void SyncMeals(MealPlanDay day, MealPlanDayDto dto)
        {
            CollectionSyncHelper.Sync(
                day.Meals,
                dto.Meals ?? [],
                db => db.Id,
                d => d.Id ?? Guid.Empty,
                d => new MealPlanMeal
                {
                    Period = d.Period!.Value,
                    Time = d.Time!,
                    Description = d.Description!,
                    Quantity = d.Quantity!,
                    Notes = d.Notes,
                    MealPlanDayId = day.Id,
                    CreatedAt = DateTimeOffset.UtcNow
                },
                (db, d) =>
                {
                    db.Period = d.Period!.Value;
                    db.Time = d.Time!;
                    db.Description = d.Description!;
                    db.Quantity = d.Quantity!;
                    db.Notes = d.Notes;
                    db.UpdatedAt = DateTimeOffset.UtcNow;
                },
                db =>
                {
                    db.IsDeleted = true;
                    db.DeletedAt = DateTimeOffset.UtcNow;
                }
            );
        }

    }
}
