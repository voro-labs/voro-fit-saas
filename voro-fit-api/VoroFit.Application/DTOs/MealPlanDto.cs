using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class MealPlanDto
    {
        public Guid? Id { get; set; }
        public MealPlanStatusEnum? Status { get; set; }

        public Guid? StudentId { get; set; }
        public StudentDto? Student { get; set; }

        public ICollection<MealPlanDayDto>? Days { get; set; } = [];

        public DateTimeOffset? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}