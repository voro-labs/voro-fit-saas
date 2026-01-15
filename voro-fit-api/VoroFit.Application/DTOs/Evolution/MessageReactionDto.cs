using System.Text.Json.Serialization;
using VoroFit.Domain.Entities.Evolution;

namespace VoroFit.Application.DTOs.Evolution
{
    public class MessageReactionDto
    {
        public Guid? Id { get; set; }

        public string? Reaction { get; set; }

        public string? RemoteFrom { get; set; }

        public string? RemoteTo { get; set; }

        public bool? IsFromMe { get; set; }

        public DateTimeOffset? SentAt { get; set; }

        public Guid? ContactId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Contact? Contact { get; set; }

        public Guid? MessageId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public Message? Message { get; set; }
    }
}
