namespace VoroFit.Application.DTOs
{
    public class WorkoutPlanWeekDto
    {
        public Guid? Id { get; set; }

        public int? WeekNumber { get; set; }

        public Guid? WorkoutPlanId { get; set; }
        public WorkoutPlanDto? WorkoutPlan { get; set; }

        public ICollection<WorkoutPlanDayDto>? Days { get; set; } = [];

        public ICollection<WorkoutHistoryDto>? WorkoutHistories { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
