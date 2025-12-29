namespace VoroFit.Application.DTOs
{
    public class WorkoutHistoryExerciseDto
    {
        public Guid? Id { get; set; }

        public Guid? WorkoutHistoryId { get; set; }
        public WorkoutHistoryDto? WorkoutHistory { get; set; } = null!;

        public Guid? ExerciseId { get; set; }
        public ExerciseDto? Exercise { get; set; } = null!;

        public int? Order { get; set; }

        public int? PlannedSets { get; set; }
        public int? PlannedReps { get; set; }

        public int? ExecutedSets { get; set; }
        public int? ExecutedReps { get; set; }

        public float? PlannedWeight { get; set; }
        public float? ExecutedWeight { get; set; }

        public int? RestInSeconds { get; set; }

        public string? Notes { get; set; }
    }
}
