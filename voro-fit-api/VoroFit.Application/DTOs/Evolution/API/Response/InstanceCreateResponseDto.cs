using System.Text.Json.Serialization;

namespace VoroFit.Application.DTOs.Evolution.API.Response
{
    public class InstanceCreateResponseDto
    {
        [JsonPropertyName("instance")]
        public InstanceJsonDto? Instance { get; set; }

        [JsonPropertyName("qrcode")]
        public QrCodeJsonDto? Qrcode { get; set; }

        [JsonPropertyName("hash")]
        public string? Hash { get; set; }
    }

    public class InstanceJsonDto
    {
        [JsonPropertyName("instanceId")]
        public Guid? InstanceId { get; set; }

        [JsonPropertyName("instanceName")]
        public string? InstanceName { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("integration")]
        public string? Integration { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }

    public class QrCodeJsonDto
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("base64")]
        public string? Base64 { get; set; }

        [JsonPropertyName("pairingCode")]
        public string? PairingCode { get; set; }

        [JsonPropertyName("count")]
        public int? Count { get; set; }
    }
}
