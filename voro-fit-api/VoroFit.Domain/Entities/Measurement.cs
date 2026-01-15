using VoroFit.Domain.Interfaces.Entities;

namespace VoroFit.Domain.Entities
{
    public class Measurement : ISoftDeletable
    {
        public Guid Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public float Weight { get; set; }
        public float Waist { get; set; }
        public float Chest { get; set; }
        public float Arms { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }

}
