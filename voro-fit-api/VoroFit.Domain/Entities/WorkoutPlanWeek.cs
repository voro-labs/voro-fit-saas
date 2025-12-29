using VoroFit.Domain.Interfaces.Entities;

namespace VoroFit.Domain.Entities
{
    public class WorkoutPlanWeek : ISoftDeletable
    {
        public Guid Id { get; set; }

        public int WeekNumber { get; set; } // Semana 1, 2, 3...

        public Guid WorkoutPlanId { get; set; }
        public WorkoutPlan WorkoutPlan { get; set; } = null!;

        public ICollection<WorkoutPlanDay> Days { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
