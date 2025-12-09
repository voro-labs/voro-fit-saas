using VoroFit.Domain.Entities;

namespace VoroFit.Application.DTOs
{
    public class WorkoutExerciseDto
    {
        public Guid? Id { get; set; } = null;

        public Guid StudentId { get; set; }
        public StudentDto? Student { get; set; } = null;

        public Guid WorkoutHistoryId { get; set; }
        public WorkoutHistoryDto? WorkoutHistory { get; set; } = null;

        public Guid ExerciseId { get; set; }
        public ExerciseDto? Exercise { get; set; } = null;

        public int Order { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int RestInSeconds { get; set; }
        public float? Weight { get; set; }

        public string? Notes { get; set; }
        public string? Alternative { get; set; }
    }
}