using VoroFit.Domain.Enums;
using VoroFit.Domain.Interfaces.Entities;

namespace VoroFit.Domain.Entities
{
    public class WorkoutPlan : ISoftDeletable
    {
        public Guid Id { get; set; }

        public WorkoutPlanStatusEnum Status { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public ICollection<WorkoutPlanWeek> Weeks { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
