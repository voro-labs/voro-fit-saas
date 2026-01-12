using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs.Evolution
{
    public class ContactDto
    {
        public Guid? Id { get; set; }

        public string? RemoteJid { get; set; }

        public string? Number { get; set; }

        public string? DisplayName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public IFormFile? ProfilePicture { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public string? LastKnownPresence { get; set; }
        public DateTimeOffset? LastPresenceAt { get; set; }

        public Guid? UserExtensionId { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public UserExtensionDto? UserExtension { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public ICollection<ContactIdentifierDto>? Identifiers { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public ICollection<GroupMemberDto>? GroupMemberships { get; set; }
    }
}