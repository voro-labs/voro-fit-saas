namespace VoroFit.Application.DTOs
{
    public class WorkoutPlanWeekDto
    {
        public Guid? Id { get; set; }

        public int? WeekNumber { get; set; }

        public Guid? WorkoutPlanId { get; set; }
        public WorkoutPlanDto? WorkoutPlan { get; set; }

        public ICollection<WorkoutPlanDayDto>? Days { get; set; } = [];

        public DateTimeOffset? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
