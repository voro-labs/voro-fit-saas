using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class ExerciseDto
    {
        public Guid? Id { get; set; } = null;
        public string Name { get; set; } = null!;
        public string MuscleGroup { get; set; } = null!;
        public ExerciseTypeEnum Type { get; set; }
        public string Thumbnail { get; set; } = null!;

        public string? Description { get; set; }
        public string? Notes { get; set; }
        public string? Alternatives { get; set; }

        public DateTimeOffset? CreatedAt { get; set; } = null;
        public DateTimeOffset? UpdatedAt { get; set; } = null;

        public ICollection<WorkoutExerciseDto> WorkoutExercises { get; set; } = [];
    }
}