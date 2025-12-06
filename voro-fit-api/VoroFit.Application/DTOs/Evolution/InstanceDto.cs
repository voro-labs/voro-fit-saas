using System.Text.Json.Serialization;

namespace VoroFit.Application.DTOs.Evolution
{
    public class InstanceDto
    {
        public string Id { get; set; } = null!;
        public string ExternalId { get; set; } = string.Empty;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public ICollection<ChatDto> Chats { get; set; } = [];
    }
}
