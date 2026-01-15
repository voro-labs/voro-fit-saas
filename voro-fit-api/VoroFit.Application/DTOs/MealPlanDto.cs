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

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}