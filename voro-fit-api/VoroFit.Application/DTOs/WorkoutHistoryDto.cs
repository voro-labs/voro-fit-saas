using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class WorkoutHistoryDto
    {
        public Guid? Id { get; set; }

        // aluno que executou o treino
        public Guid? StudentId { get; set; }
        public StudentDto? Student { get; set; }

        // vínculo com o planejamento
        public Guid? WorkoutPlanDayId { get; set; }
        public WorkoutPlanDayDto? WorkoutPlanDay { get; set; }

        // informações da execução
        public DateTimeOffset? ExecutionDate { get; set; } = DateTime.UtcNow;

        public WorkoutExecutionStatusEnum? Status { get; set; } 
            = WorkoutExecutionStatusEnum.Completed;

        public string? Notes { get; set; }

        public ICollection<WorkoutHistoryExerciseDto>? Exercises { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}