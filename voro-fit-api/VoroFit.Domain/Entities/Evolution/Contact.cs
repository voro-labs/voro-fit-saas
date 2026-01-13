using VoroFit.Domain.Enums;

namespace VoroFit.Domain.Entities.Evolution
{
    public class Contact
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // JID principal (normalmente o @s.whatsapp.net)
        public string RemoteJid { get; set; } = string.Empty;

        public string Number { get; set; } = string.Empty;

        public string? DisplayName { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        public string? LastKnownPresence { get; set; }
        public DateTimeOffset LastPresenceAt { get; set; } = DateTimeOffset.UtcNow;

        public Guid? UserExtensionId { get; set; }
        public UserExtension? UserExtension { get; set; }

        public ICollection<MessageReaction> MessageReactions { get; set; } = [];
        public ICollection<ContactIdentifier> Identifiers { get; set; } = [];
        public ICollection<GroupMember> GroupMemberships { get; set; } = [];
        public ICollection<Chat> Chats { get; set; } = [];
    }
}
