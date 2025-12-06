namespace VoroFit.Domain.Entities
{
    public class MealPlanDay
    {
        public Guid Id { get; set; }
        public string DayOfWeek { get; set; } = null!; // Segunda, Terça...

        public Guid MealPlanId { get; set; }
        public MealPlan MealPlan { get; set; } = null!;

        public ICollection<MealPlanMeal> Meals { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
