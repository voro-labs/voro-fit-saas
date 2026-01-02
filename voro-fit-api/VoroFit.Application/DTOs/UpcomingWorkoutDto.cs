namespace VoroFit.Application.DTOs
{
    public class UpcomingWorkoutDto
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string? StudentAvatar { get; set; }
        public DateTimeOffset Time { get; set; }
        public string WorkoutType { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
    }
}
