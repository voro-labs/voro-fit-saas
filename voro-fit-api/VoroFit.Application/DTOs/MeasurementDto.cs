namespace VoroFit.Application.DTOs
{
    public class MeasurementDto
    {
        public Guid? Id { get; set; }

        public DateTimeOffset? Date { get; set; }

        public float? Weight { get; set; }
        public float? Waist { get; set; }
        public float? Chest { get; set; }
        public float? Arms { get; set; }

        public Guid? StudentId { get; set; }
        public StudentDto? Student { get; set; }


        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
