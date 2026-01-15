using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace VoroFit.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum IntegrationTypeEnum
    {
        [EnumMember(Value = "WHATSAPP-BAILEYS")]
        WhatsAppBaileys,

        [EnumMember(Value = "WHATSAPP-BUSINESS")]
        WhatsAppBusiness,

        [EnumMember(Value = "EVOLUTION")]
        Evolution
    }
}
