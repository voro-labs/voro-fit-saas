using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class MealPlanDto
    {
        public Guid? Id { get; set; } = null;
        public MealPlanStatusEnum Status { get; set; }

        public Guid StudentId { get; set; }
        public StudentDto? Student { get; set; } = null;

        public DateTimeOffset? LastUpdated { get; set; } = null;

        public ICollection<MealPlanDayDto> Days { get; set; } = [];

        public DateTimeOffset? CreatedAt { get; set; } = null;
        public DateTimeOffset? UpdatedAt { get; set; } = null;
    }
}