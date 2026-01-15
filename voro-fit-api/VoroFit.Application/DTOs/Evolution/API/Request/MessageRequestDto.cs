using System.Text.Json.Serialization;

namespace VoroFit.Application.DTOs.Evolution.API.Request
{
    public class MessageRequestDto
    {
        public string Number { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public QuotedRequestDto? Quoted { get; set; } = null;
    }
}
