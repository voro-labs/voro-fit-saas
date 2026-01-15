using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalStudents { get; set; }
        public int StudentsChangeThisMonth { get; set; }
        public int ActiveWorkouts { get; set; }
        public int PendingWorkouts { get; set; }
        public decimal AdherenceRate { get; set; }
        public decimal AdherenceChange { get; set; }
    }
}
