namespace VoroFit.Application.DTOs
{
    public class MeasurementDto
    {
        public Guid? Id { get; set; } = null;

        public DateTimeOffset? Date { get; set; } = null;

        public float? Weight { get; set; }
        public float? Waist { get; set; }
        public float? Chest { get; set; }
        public float? Arms { get; set; }

        public Guid? StudentId { get; set; }
        public StudentDto? Student { get; set; } = null;


        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
