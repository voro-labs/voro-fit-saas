using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class MealPlanDayDto
    {
        public Guid? Id { get; set; }
        public DayOfWeekEnum? DayOfWeek { get; set; }

        public Guid? MealPlanId { get; set; }
        public MealPlanDto? MealPlan { get; set; }

        public ICollection<MealPlanMealDto>? Meals { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}