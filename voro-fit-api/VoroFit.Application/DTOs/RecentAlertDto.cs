using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class RecentAlertDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTimeOffset Time { get; set; }
        public AlertTypeEnum Type { get; set; }
    }
}
