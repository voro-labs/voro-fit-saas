using VoroFit.Domain.Enums;
using VoroFit.Domain.Interfaces.Entities;

namespace VoroFit.Domain.Entities
{
    public class MealPlan : ISoftDeletable
    {
        public Guid Id { get; set; }
        public MealPlanStatusEnum Status { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public ICollection<MealPlanDay> Days { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }

}
