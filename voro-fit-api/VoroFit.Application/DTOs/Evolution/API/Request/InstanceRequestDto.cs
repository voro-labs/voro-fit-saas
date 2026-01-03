using System.Text.Json.Serialization;
using VoroFit.Domain.Enums;
using VoroFit.Shared.Converters;
using VoroFit.Shared.Utils;

namespace VoroFit.Application.DTOs.Evolution.API.Request
{
    public class InstanceRequestDto
    {
        public string InstanceName { get; set; } = string.Empty;
        public bool Qrcode { get; set; } = true;
        [JsonConverter(typeof(JsonStringEnumMemberConverter<IntegrationTypeEnum>))]
        public IntegrationTypeEnum Integration { get; set; } = IntegrationTypeEnum.WhatsAppBaileys;
        public InstanceWebhookRequestDto Webhook { get; set; } = new();

        public void SetWebhookUrl(EvolutionUtil evolutionUtil)
        {
            this.Webhook.Url = evolutionUtil.BaseWebhookUrl;
        }
    }

    public class InstanceWebhookRequestDto
    {
        public string Url { get; set; } = "https://fit.vorolabs.app/api/v1/webhook";
        public bool ByEvents { get; set; } = false;
        public bool Base64 { get; set; } = true;
        public List<string> Events { get; set; } = [
            "APPLICATION_STARTUP",
            "QRCODE_UPDATED",
            "MESSAGES_SET",
            "MESSAGES_UPSERT",
            "MESSAGES_UPDATE",
            "MESSAGES_DELETE",
            "SEND_MESSAGE",
            "CONTACTS_SET",
            "CONTACTS_UPSERT",
            "CONTACTS_UPDATE",
            "PRESENCE_UPDATE",
            "CHATS_SET",
            "CHATS_UPSERT",
            "CHATS_UPDATE",
            "CHATS_DELETE",
            "GROUPS_UPSERT",
            "GROUP_UPDATE",
            "GROUP_PARTICIPANTS_UPDATE",
            "CONNECTION_UPDATE",
            "LABELS_EDIT",
            "LABELS_ASSOCIATION",
            "CALL",
            "TYPEBOT_START",
            "TYPEBOT_CHANGE_STATUS"
        ];
    }
}
