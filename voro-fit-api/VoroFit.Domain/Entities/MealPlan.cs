using VoroFit.Domain.Enums;

namespace VoroFit.Domain.Entities
{
    public class MealPlan
    {
        public Guid Id { get; set; }
        public MealPlanStatusEnum Status { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public DateTimeOffset LastUpdated { get; set; } = DateTime.UtcNow;

        public ICollection<MealPlanDay> Days { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
    }

}
