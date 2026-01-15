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
        public UserExtensionDto? UserExtension { get; set; }
        
        public ICollection<MessageReactionDto>? MessageReactions { get; set; }
        public ICollection<ContactIdentifierDto>? Identifiers { get; set; }
        public ICollection<GroupMemberDto>? GroupMemberships { get; set; }
        public ICollection<ChatDto>? Chats { get; set; }
    }
}