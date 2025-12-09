using VoroFit.Domain.Entities;
using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class WorkoutHistoryDto
    {
        public Guid? Id { get; set; } = null;

        public string Name { get; set; } = null!;

        // aluno dono do treino
        public Guid StudentId { get; set; }
        public Student? Student { get; set; } = null;

        public DateTimeOffset? CreatedDate { get; set; } = null;
        public DateTimeOffset? LastUpdated { get; set; } = null;

        public WorkoutStatusEnum Status { get; set; } = WorkoutStatusEnum.Active;

        public ICollection<WorkoutExercise> Exercises { get; set; } = [];
    }
}