using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class MealPlanMealDto
    {
        public Guid? Id { get; set; }
        public MealPeriodEnum? Period { get; set; }
        public string? Time { get; set; } // 07:00
        public string? Description { get; set; }
        public string? Quantity { get; set; }
        public string? Notes { get; set; }

        public Guid? MealPlanDayId { get; set; }
        public MealPlanDayDto? MealPlanDay { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}