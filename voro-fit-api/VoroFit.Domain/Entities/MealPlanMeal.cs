using VoroFit.Domain.Interfaces.Entities;

namespace VoroFit.Domain.Entities
{
    public class MealPlanMeal : ISoftDeletable
    {
        public Guid Id { get; set; }
        public string Period { get; set; } = null!;
        public string Time { get; set; } = null!; // 07:00
        public string Description { get; set; } = null!;
        public string Quantity { get; set; } = null!;
        public string? Notes { get; set; }

        public Guid MealPlanDayId { get; set; }
        public MealPlanDay MealPlanDay { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }

}
