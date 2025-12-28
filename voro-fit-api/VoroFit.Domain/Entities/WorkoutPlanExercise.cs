namespace VoroFit.Domain.Entities
{
    public class WorkoutPlanExercise
    {
        public Guid Id { get; set; }

        public Guid WorkoutPlanDayId { get; set; }
        public WorkoutPlanDay WorkoutPlanDay { get; set; } = null!;

        public Guid ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;

        public int Order { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int RestInSeconds { get; set; }
        public float? Weight { get; set; }

        public string? Notes { get; set; }
        public string? Alternative { get; set; }
    }
}