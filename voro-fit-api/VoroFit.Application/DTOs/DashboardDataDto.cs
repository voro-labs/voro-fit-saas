namespace VoroFit.Application.DTOs
{
    public class DashboardDataDto
    {
        public DashboardStatsDto Stats { get; set; } = null!;
        public List<UpcomingWorkoutDto> UpcomingWorkouts { get; set; } = [];
        public List<RecentAlertDto> RecentAlerts { get; set; } = [];
        public List<StudentProgressDto> StudentProgress { get; set; } = [];
    }
}
