namespace VoroFit.Domain.Entities.Evolution
{
    public class Chat
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string RemoteJid { get; set; } = string.Empty;
        public bool IsGroup { get; set; }

        public Guid InstanceExtensionId { get; set; } = Guid.NewGuid();
        public InstanceExtension InstanceExtension { get; set; } = null!;

        public DateTimeOffset LastMessageAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        public Guid? ContactId { get; set; }
        public Contact? Contact { get; set; }

        public Guid? GroupId { get; set; }
        public Group? Group { get; set; }

        public ICollection<Message> Messages { get; set; } = [];
    }
}
