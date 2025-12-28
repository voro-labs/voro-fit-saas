namespace VoroFit.Domain.Entities
{
    public class WorkoutPlanDay
    {
        public Guid Id { get; set; }

        public string DayOfWeek { get; set; } = null!; // Segunda, Terça...

        public Guid WorkoutPlanWeekId { get; set; }
        public WorkoutPlanWeek WorkoutPlanWeek { get; set; } = null!;

        public ICollection<WorkoutPlanExercise> Exercises { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
