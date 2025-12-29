using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class WorkoutPlanDayDto
    {
        public Guid? Id { get; set; }

        public DayOfWeekEnum? DayOfWeek { get; set; } // Segunda, Terça...

        public Guid? WorkoutPlanWeekId { get; set; }
        public WorkoutPlanWeekDto? WorkoutPlanWeek { get; set; } = null!;

        public ICollection<WorkoutPlanExerciseDto>? Exercises { get; set; } = [];

        public DateTimeOffset? CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
