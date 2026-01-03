namespace VoroFit.Application.DTOs
{
    public class WorkoutHistoryExerciseDto
    {
        public Guid? Id { get; set; }

        public Guid? WorkoutHistoryId { get; set; }
        public WorkoutHistoryDto? WorkoutHistory { get; set; }

        public Guid? ExerciseId { get; set; }
        public ExerciseDto? Exercise { get; set; }

        public int? Order { get; set; }

        public int? PlannedSets { get; set; }
        public int? PlannedReps { get; set; }

        public int? ExecutedSets { get; set; }
        public int? ExecutedReps { get; set; }

        public float? PlannedWeight { get; set; }
        public float? ExecutedWeight { get; set; }

        public int? RestInSeconds { get; set; }

        public string? Notes { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
