using VoroFit.Domain.Enums;

namespace VoroFit.Domain.Entities
{
    public class WorkoutPlan
    {
        public Guid Id { get; set; }

        public WorkoutPlanStatusEnum Status { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public ICollection<WorkoutPlanWeek> Weeks { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
