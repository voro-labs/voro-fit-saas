using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Domain.Entities;
using VoroFit.Domain.Enums;
using VoroFit.Domain.Interfaces.Repositories;
using VoroFit.Shared.Helpers;

namespace VoroFit.Application.Services
{
    public class WorkoutPlanService(IWorkoutPlanRepository workoutPlanRepository, IMapper mapper) : ServiceBase<WorkoutPlan>(workoutPlanRepository), IWorkoutPlanService
    {
        public async Task<WorkoutPlanDto> CreateAsync(WorkoutPlanDto dto)
        {
            var workoutPlan = mapper.Map<WorkoutPlan>(dto);

            workoutPlan.StudentId = dto.StudentId!.Value;

            SyncWeeks(workoutPlan, dto);

            await base.AddAsync(workoutPlan);

            return mapper.Map<WorkoutPlanDto>(workoutPlan);
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
            var existingWorkoutPlan = await base.GetByIdAsync(
                wp => wp.Id == id,
                wp => wp.Include(wp => wp.Student),
                wp => wp.Include(wp => wp.Weeks)
                    .ThenInclude(w => w.Days)
                    .ThenInclude(d => d.Exercises)
            ) ?? throw new Exception("WorkoutPlan não encontrado");

            mapper.Map(dto, existingWorkoutPlan);

            SyncWeeks(existingWorkoutPlan, dto);

            existingWorkoutPlan.UpdatedAt = DateTimeOffset.UtcNow;

            base.Update(existingWorkoutPlan);

            return mapper.Map<WorkoutPlanDto>(existingWorkoutPlan);
        }

        private static void SyncWeeks(WorkoutPlan plan, WorkoutPlanDto dto)
        {
            CollectionSyncHelper.Sync(
                plan.Weeks,
                dto.Weeks ?? [],
                db => db.Id,
                d => d.Id,
                d =>
                {
                    var week = new WorkoutPlanWeek
                    {
                        WorkoutPlanId = plan.Id,
                        WorkoutPlan = plan,
                        WeekNumber = d.WeekNumber ?? 0,
                        CreatedAt = DateTimeOffset.UtcNow
                    };

                    SyncDays(week, d);

                    return week;
                },
                (db, d) =>
                {
                    db.WeekNumber = d.WeekNumber ?? db.WeekNumber;
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
                d => d.Id,
                d => 
                {
                    var day = new WorkoutPlanDay
                    {
                        WorkoutPlanWeekId = week.Id,
                        WorkoutPlanWeek = week,
                        Time = d.Time!,
                        DayOfWeek = d.DayOfWeek ?? DayOfWeekEnum.Monday,
                        CreatedAt = DateTimeOffset.UtcNow
                    };

                    SyncExercises(day, d);

                    return day;
                },
                (db, d) =>
                {
                    db.DayOfWeek = d.DayOfWeek ?? db.DayOfWeek;
                    db.UpdatedAt = DateTimeOffset.UtcNow;
                    SyncExercises(db, d);
                },
                db =>
                {
                    if (db.WorkoutHistories.Any())
                    {
                        db.IsDeleted = true;
                        db.DeletedAt = DateTimeOffset.UtcNow;
                        return;
                    }

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
                d => d.Id,
                d =>
                {
                    var exercise = new WorkoutPlanExercise
                    {
                        WorkoutPlanDayId = day.Id,
                        WorkoutPlanDay = day,
                        ExerciseId = d.ExerciseId ?? Guid.Empty,
                        Order = d.Order ?? 0,
                        Sets = d.Sets ?? 0,
                        Reps = d.Reps ?? 0,
                        RestInSeconds = d.RestInSeconds ?? 0,
                        Weight = d.Weight
                    };

                    return exercise;
                },
                (db, d) =>
                {
                    db.Order = d.Order ?? db.Order;
                    db.Sets = d.Sets ?? db.Sets;
                    db.Reps = d.Reps ?? db.Reps;
                    db.RestInSeconds = d.RestInSeconds ?? db.RestInSeconds;
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
