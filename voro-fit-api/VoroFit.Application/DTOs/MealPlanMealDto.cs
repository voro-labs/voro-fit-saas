namespace VoroFit.Application.DTOs
{
    public class MealPlanMealDto
    {
        public Guid? Id { get; set; } = null;
        public string? Period { get; set; } = null!;
        public string? Time { get; set; } = null!; // 07:00
        public string? Description { get; set; } = null!;
        public string? Quantity { get; set; } = null!;
        public string? Notes { get; set; }

        public Guid? MealPlanDayId { get; set; }
        public MealPlanDayDto? MealPlanDay { get; set; } = null;

        public DateTimeOffset? CreatedAt { get; set; } = null;
        public DateTimeOffset? UpdatedAt { get; set; } = null;
    }
}