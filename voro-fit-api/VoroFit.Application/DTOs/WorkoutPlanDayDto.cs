using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class WorkoutPlanDayDto
    {
        public Guid? Id { get; set; }

        public DayOfWeekEnum? DayOfWeek { get; set; }
        public string? Time { get; set; }

        public Guid? WorkoutPlanWeekId { get; set; }
        public WorkoutPlanWeekDto? WorkoutPlanWeek { get; set; }

        public ICollection<WorkoutPlanExerciseDto>? Exercises { get; set; } = [];

        public ICollection<WorkoutHistoryDto>? WorkoutHistories { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
