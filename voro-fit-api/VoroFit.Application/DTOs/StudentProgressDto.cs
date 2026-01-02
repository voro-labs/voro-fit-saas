namespace VoroFit.Application.DTOs
{
    public class StudentProgressDto
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string? StudentAvatar { get; set; }
        public decimal Progress { get; set; }
        public string Goal { get; set; } = string.Empty;
    }
}
