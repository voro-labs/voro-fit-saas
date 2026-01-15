using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class ExerciseDto
    {
        public Guid? Id { get; set; }

        public Guid? TrainerId { get; set; }
        public UserExtensionDto? Trainer { get; set; }

        public string? Name { get; set; }
        public string? MuscleGroup { get; set; }
        public ExerciseTypeEnum? Type { get; set; }

        public string? Thumbnail { get; set; }
        
        public string? MediaUrl { get; set; }
        public string? Media { get; set; }

        public string? Description { get; set; }
        public string? Notes { get; set; }
        public string? Alternatives { get; set; }

        public ICollection<WorkoutPlanExerciseDto>? WorkoutPlanExercises { get; set; } = [];
        public ICollection<WorkoutHistoryExerciseDto>? WorkoutHistoryExercises { get; set; } = [];

        public DateTimeOffset? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool? IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}