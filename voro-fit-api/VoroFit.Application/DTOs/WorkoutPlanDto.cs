using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class WorkoutPlanDto
    {
        public Guid? Id { get; set; }

        public WorkoutPlanStatusEnum? Status { get; set; }

        public Guid? StudentId { get; set; }
        public StudentDto? Student { get; set; }

        public ICollection<WorkoutPlanWeekDto>? Weeks { get; set; } = [];

        public DateTimeOffset? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
