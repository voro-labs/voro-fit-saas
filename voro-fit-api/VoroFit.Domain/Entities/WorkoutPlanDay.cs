using VoroFit.Domain.Enums;
using VoroFit.Domain.Interfaces.Entities;

namespace VoroFit.Domain.Entities
{
    public class WorkoutPlanDay : ISoftDeletable
    {
        public Guid Id { get; set; }

        public DayOfWeekEnum DayOfWeek { get; set; } = DayOfWeekEnum.Sunday; // Segunda, Terça...

        public Guid WorkoutPlanWeekId { get; set; }
        public WorkoutPlanWeek WorkoutPlanWeek { get; set; } = null!;

        public ICollection<WorkoutPlanExercise> Exercises { get; set; } = [];
        public ICollection<WorkoutHistory> WorkoutHistories { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
