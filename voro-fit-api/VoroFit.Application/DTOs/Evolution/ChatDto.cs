using System.Text.Json.Serialization;
using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs.Evolution
{
    public class ChatDto
    {
        public Guid? Id { get; set; }
        public string? RemoteJid { get; set; }
        public bool? IsGroup { get; set; }

        public Guid? InstanceExtensionId { get; set; }
        public InstanceExtensionDto? InstanceExtension { get; set; }

        public string? LastMessage { get; set; }
        public bool? LastMessageFromMe { get; set; }
        public MessageStatusEnum? LastMessageStatus { get; set; }
        public DateTimeOffset? LastMessageAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public Guid? ContactId { get; set; }
        public ContactDto? Contact { get; set; }

        public Guid? GroupId { get; set; }
        public GroupDto? Group { get; set; }
        
        public ICollection<MessageDto>? Messages { get; set; }
    }
}