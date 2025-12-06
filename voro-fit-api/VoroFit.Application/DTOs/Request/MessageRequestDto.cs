using System.Text.Json.Serialization;

namespace VoroFit.Application.DTOs.Request
{
    public class MessageRequestDto
    {
        public string Number { get; set; } = string.Empty;
        public string Conversation { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public QuotedRequestDto? Quoted { get; set; } = null;
    }
}
