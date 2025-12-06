using VoroFit.Domain.Enums;

namespace VoroFit.Domain.Entities
{
    public class WorkoutHistory
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        // aluno dono do treino
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        public DateTimeOffset CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTimeOffset LastUpdated { get; set; } = DateTime.UtcNow;

        public WorkoutStatusEnum Status { get; set; } = WorkoutStatusEnum.Active;

        public ICollection<WorkoutExercise> Exercises { get; set; } = [];
    }
}
