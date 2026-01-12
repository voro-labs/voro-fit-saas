using System.Text.Json.Serialization;
using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs.Evolution
{
    public class MessageDto
    {
        public Guid? Id { get; set; }

        public string? ExternalId { get; set; }
        public string? RemoteFrom { get; set; }
        public string? RemoteTo { get; set; }

        public string? Content { get; set; }
        public string? Base64 { get; set; }
        public string? RawJson { get; set; }

        public DateTimeOffset? SentAt { get; set; }
        public bool? IsFromMe { get; set; }

        public MessageStatusEnum? Status { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public MessageTypeEnum? Type { get; set; }

        public string? MimeType { get; set; }
        public string? FileUrl { get; set; }
        public long? FileLength { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? DurationSeconds { get; set; }
        public byte[]? Thumbnail { get; set; }

        public Guid? ChatId { get; set; }
        public ChatDto? Chat { get; set; }

        public Guid? QuotedMessageId { get; set; }
        public MessageDto? QuotedMessage { get; set; }
        
        public ICollection<MessageReactionDto> MessageReactions { get; set; } = [];
    }
}