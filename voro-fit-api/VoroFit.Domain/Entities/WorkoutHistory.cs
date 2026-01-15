using VoroFit.Domain.Enums;
using VoroFit.Domain.Interfaces.Entities;

namespace VoroFit.Domain.Entities
{
    public class WorkoutHistory : ISoftDeletable
    {
        public Guid Id { get; set; }

        // aluno que executou o treino
        public Guid StudentId { get; set; }
        public Student Student { get; set; } = null!;

        // vínculo com o planejamento
        public Guid WorkoutPlanDayId { get; set; }
        public WorkoutPlanDay WorkoutPlanDay { get; set; } = null!;

        // informações da execução
        public DateTimeOffset ExecutionDate { get; set; } = DateTime.UtcNow;

        public WorkoutExecutionStatusEnum Status { get; set; } 
            = WorkoutExecutionStatusEnum.Completed;

        public string? Notes { get; set; }

        public ICollection<WorkoutHistoryExercise> Exercises { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
