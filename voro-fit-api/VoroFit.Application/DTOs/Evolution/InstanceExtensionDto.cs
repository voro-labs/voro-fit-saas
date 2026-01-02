using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs.Evolution
{
    public class InstanceExtensionDto
    {
        public Guid? InstanceId { get; set; }
        public InstanceDto? Instance { get; set; }

        public string? Hash { get; set; }
        public string? Base64 { get; set; }
        public string? Integration { get; set; }

        public string? PhoneNumber { get; set; }
        public InstanceStatusEnum? Status { get; set; }

        public DateTimeOffset? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? ConnectedAt { get; set; }

        public ICollection<ChatDto>? Chats { get; set; }
    }
}
