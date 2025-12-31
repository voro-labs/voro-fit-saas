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
    public class WorkoutPlanService(IWorkoutPlanRepository workoutPlanRepository, IMapper mapper) : ServiceBase<WorkoutPlan>(workoutPlanRepository), IWorkoutPlanService
    {
        public async Task<WorkoutPlanDto> CreateAsync(WorkoutPlanDto dto)
        {
            var createWorkoutPlanDto = mapper.Map<WorkoutPlan>(dto);

            await base.AddAsync(createWorkoutPlanDto);

            return mapper.Map<WorkoutPlanDto>(createWorkoutPlanDto);
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
                            .ThenInclude(w => w.Exercise)
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
                            .ThenInclude(w => w.Exercise)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            return mapper.Map<WorkoutPlanDto?>(workoutPlan);
        }

        public async Task<WorkoutPlanDto> UpdateAsync(Guid id, WorkoutPlanDto dto)
        {
            var existingWorkoutPlan = await base.GetByIdAsync(wp => 
                wp.Id == id,
                wp => wp.Include(wp => wp.Student),
                wp => wp.Include(wp => wp.Weeks)
                    .ThenInclude(w => w.Days)
                    .ThenInclude(d => d.Exercises)) 
                ?? throw new Exception("WorkoutPlan não encontrado");
                
            mapper.Map(dto, existingWorkoutPlan);

            SyncWeeks(existingWorkoutPlan, dto);

            base.Update(existingWorkoutPlan);

            return mapper.Map<WorkoutPlanDto>(existingWorkoutPlan);
        }

        private static void SyncWeeks(WorkoutPlan plan, WorkoutPlanDto dto)
        {
            CollectionSyncHelper.Sync(
                plan.Weeks,
                dto.Weeks ?? [],
                db => db.Id,
                d => d.Id ?? Guid.Empty,
                d => new WorkoutPlanWeek
                {
                    Id = Guid.NewGuid(),
                    WeekNumber = d.WeekNumber ?? 0,
                    CreatedAt = DateTimeOffset.UtcNow
                },
                (db, d) =>
                {
                    db.WeekNumber = d.WeekNumber ?? 0;
                    db.UpdatedAt = DateTimeOffset.UtcNow;
                    SyncDays(db, d);
                },
                db =>
                {
                    db.IsDeleted = true;
                    db.DeletedAt = DateTimeOffset.UtcNow;
                }
            );
        }

        private static void SyncDays(WorkoutPlanWeek week, WorkoutPlanWeekDto dto)
        {
            CollectionSyncHelper.Sync(
                week.Days,
                dto.Days ?? [],
                db => db.Id,
                d => d.Id ?? Guid.Empty,
                d => new WorkoutPlanDay
                {
                    Id = Guid.NewGuid(),
                    DayOfWeek = d.DayOfWeek ?? Domain.Enums.DayOfWeekEnum.Monday,
                    CreatedAt = DateTimeOffset.UtcNow
                },
                (db, d) =>
                {
                    db.DayOfWeek = d.DayOfWeek ?? Domain.Enums.DayOfWeekEnum.Monday;
                    db.UpdatedAt = DateTimeOffset.UtcNow;
                    SyncExercises(db, d);
                },
                db =>
                {
                    db.IsDeleted = true;
                    db.DeletedAt = DateTimeOffset.UtcNow;
                }
            );
        }

        private static void SyncExercises(WorkoutPlanDay day, WorkoutPlanDayDto dto)
        {
            CollectionSyncHelper.Sync(
                day.Exercises,
                dto.Exercises ?? [],
                db => db.Id,
                d => d.Id ?? Guid.Empty,
                d => new WorkoutPlanExercise
                {
                    Id = Guid.NewGuid(),
                    ExerciseId = d.ExerciseId ?? Guid.Empty,
                    Order = d.Order ?? 0,
                    Sets = d.Sets ?? 0,
                    Reps = d.Reps ?? 0,
                    RestInSeconds = d.RestInSeconds ?? 0,
                    Weight = d.Weight
                },
                (db, d) =>
                {
                    db.Order = d.Order ?? 0;
                    db.Sets = d.Sets ?? 0;
                    db.Reps = d.Reps ?? 0;
                    db.RestInSeconds = d.RestInSeconds ?? 0;
                    db.Weight = d.Weight;
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
