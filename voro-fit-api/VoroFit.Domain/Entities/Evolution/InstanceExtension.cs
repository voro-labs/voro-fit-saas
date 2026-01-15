using System.ComponentModel.DataAnnotations;
using VoroFit.Domain.Enums;

namespace VoroFit.Domain.Entities.Evolution
{
    public class InstanceExtension
    {
        [Key]
        public Guid InstanceId { get; set; }
        public Instance Instance { get; set; } = null!;

        public string Hash { get; set; } = string.Empty;
        public string Base64 { get; set; } = string.Empty;
        public string Integration { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
        public InstanceStatusEnum Status { get; set; } = InstanceStatusEnum.Disconnected;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? ConnectedAt { get; set; }
        
        public ICollection<Chat> Chats { get; set; } = [];
    }
}