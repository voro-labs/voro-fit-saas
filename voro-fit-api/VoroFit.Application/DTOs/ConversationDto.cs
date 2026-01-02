namespace VoroFit.Application.DTOs
{
    public class ConversationDto
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string? Number { get; set; }

        public DateTimeOffset? LastMessageAt { get; set; }

        public string? LastMessage { get; set; }
    }
}
