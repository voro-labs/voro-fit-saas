using System.Text.Json.Serialization;

namespace VoroFit.Application.DTOs.Evolution.API.Response
{
    public class InstanceResponseDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("connectionStatus")]
        public string? ConnectionStatus { get; set; }

        [JsonPropertyName("integration")]
        public string? Integration { get; set; }

        [JsonPropertyName("number")]
        public string? Number { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("Setting")]
        public InstanceSettingDto? Setting { get; set; }

        [JsonPropertyName("_count")]
        public InstanceCountDto? Count { get; set; }
    }

    public class InstanceSettingDto
    {
        [JsonPropertyName("rejectCall")]
        public bool RejectCall { get; set; }

        [JsonPropertyName("groupsIgnore")]
        public bool GroupsIgnore { get; set; }

        [JsonPropertyName("alwaysOnline")]
        public bool AlwaysOnline { get; set; }
    }

    public class InstanceCountDto
    {
        [JsonPropertyName("Message")]
        public int Message { get; set; }

        [JsonPropertyName("Contact")]
        public int Contact { get; set; }

        [JsonPropertyName("Chat")]
        public int Chat { get; set; }
    }
}
