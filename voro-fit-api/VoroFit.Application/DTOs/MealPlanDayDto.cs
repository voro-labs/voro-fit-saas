namespace VoroFit.Application.DTOs
{
    public class MealPlanDayDto
    {
        public Guid? Id { get; set; } = null;
        public string DayOfWeek { get; set; } = string.Empty;

        public Guid MealPlanId { get; set; }
        public MealPlanDto? MealPlan { get; set; } = null;

        public ICollection<MealPlanMealDto> Meals { get; set; } = [];

        public DateTimeOffset? CreatedAt { get; set; } = null;
        public DateTimeOffset? UpdatedAt { get; set; } = null;
    }
}