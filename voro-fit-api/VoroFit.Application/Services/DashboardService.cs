using Microsoft.EntityFrameworkCore;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Domain.Enums;
using VoroFit.Domain.Interfaces.Repositories;

namespace VoroFit.Application.Services
{
    public class DashboardService(IStudentRepository studentRepository, IWorkoutPlanRepository workoutPlanRepository) : IDashboardService
    {
        public async Task<DashboardDataDto> GetAllAsync()
        {
            var students = await studentRepository.GetAllAsync();
            
            var workoutPlans = await workoutPlanRepository.GetAllAsync(
                wp => wp.Status == WorkoutPlanStatusEnum.Active,
                false,
                wp => wp.Include(wp => wp.Student)
                    .ThenInclude(s => s.UserExtension)
                    .ThenInclude(ue => ue.User),
                wp => wp.Include(wp => wp.Weeks)
                    .ThenInclude(w => w.Days)
                    .ThenInclude(d => d.Exercises)
            );

            var now = DateTimeOffset.UtcNow;
            var startOfMonth = new DateTimeOffset(
                now.Year,
                now.Month,
                1,
                0,
                0,
                0,
                TimeSpan.Zero
            );

            var totalStudents = students.Count();

            var studentsChangeThisMonth = students
                .Count(s => s.UserExtension.User.CreatedAt >= startOfMonth);

            var activeWorkouts = workoutPlans
                .Count(wp => !wp.IsDeleted);

            var pendingWorkouts = workoutPlans
                .Count(wp => wp.IsDeleted);

            var adherenceRate = activeWorkouts == 0
                ? 0
                : (decimal)activeWorkouts / workoutPlans.Count() * 100;

            var adherenceChange = 0m;

            var upcomingWorkouts = workoutPlans
                .Where(wp => !wp.IsDeleted)
                .SelectMany(wp => wp.Weeks)
                .SelectMany(w => w.Days)
                .Where(d => d.CreatedAt >= now)
                .Take(5)
                .Select(d => new UpcomingWorkoutDto
                {
                    Id = d.Id,
                    StudentId = d.WorkoutPlanWeek.WorkoutPlan.StudentId,
                    StudentName = $"{d.WorkoutPlanWeek.WorkoutPlan.Student.UserExtension.User.FirstName} {d.WorkoutPlanWeek.WorkoutPlan.Student.UserExtension.User.LastName}",
                    StudentAvatar = d.WorkoutPlanWeek.WorkoutPlan.Student.UserExtension.User.AvatarUrl,
                    Time = d.CreatedAt,
                    WorkoutType = "Treino",
                    Date = DateOnly.FromDateTime(d.CreatedAt.UtcDateTime)
                })
                .ToList();

            var studentProgress = students
                .Take(5)
                .Select(s => new StudentProgressDto
                {
                    StudentId = s.UserExtensionId,
                    StudentName = $"{s.UserExtension.User.FirstName} {s.UserExtension.User.LastName}",
                    StudentAvatar = s.UserExtension.User.AvatarUrl,
                    Progress = 0,
                    Goal = "Manter consistência"
                })
                .ToList();

            var recentAlerts = new List<RecentAlertDto>();

            return new DashboardDataDto
            {
                Stats = new DashboardStatsDto
                {
                    TotalStudents = totalStudents,
                    StudentsChangeThisMonth = studentsChangeThisMonth,
                    ActiveWorkouts = activeWorkouts,
                    PendingWorkouts = pendingWorkouts,
                    AdherenceRate = adherenceRate,
                    AdherenceChange = adherenceChange
                },
                UpcomingWorkouts = upcomingWorkouts,
                RecentAlerts = recentAlerts,
                StudentProgress = studentProgress
            };
        }
    }
}
