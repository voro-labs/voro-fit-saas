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
        public ICollection<WorkoutHistoryDto>? WorkoutHistories { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
