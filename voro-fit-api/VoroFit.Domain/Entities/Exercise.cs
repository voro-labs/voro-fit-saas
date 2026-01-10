using VoroFit.Domain.Enums;
using VoroFit.Domain.Interfaces.Entities;

namespace VoroFit.Domain.Entities
{
    public class Exercise : ISoftDeletable
    {
        public Guid Id { get; set; }

        public Guid? TrainerId { get; set; }
        public UserExtension? Trainer { get; set; }

        public string Name { get; set; } = string.Empty;
        public string MuscleGroup { get; set; } = string.Empty;
        public ExerciseTypeEnum Type { get; set; }

        public string Thumbnail { get; set; } = string.Empty;
        
        public string? MediaUrl { get; set; }
        public string? Media { get; set; }

        public string? Description { get; set; }
        public string? Notes { get; set; }
        public string? Alternatives { get; set; }

        public ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; set; } = [];
        public ICollection<WorkoutHistoryExercise> WorkoutHistoryExercises { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
