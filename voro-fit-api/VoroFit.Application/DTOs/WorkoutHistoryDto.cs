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
        public Guid? WorkoutPlanId { get; set; }
        public WorkoutPlanDto? WorkoutPlan { get; set; }

        public Guid? WorkoutPlanWeekId { get; set; }
        public WorkoutPlanWeekDto? WorkoutPlanWeek { get; set; }

        public Guid? WorkoutPlanDayId { get; set; }
        public WorkoutPlanDayDto? WorkoutPlanDay { get; set; }

        // informações da execução
        public DateTimeOffset? ExecutionDate { get; set; } = DateTime.UtcNow;

        public WorkoutExecutionStatusEnum? Status { get; set; } 
            = WorkoutExecutionStatusEnum.Completed;

        public string? Notes { get; set; }

        public ICollection<WorkoutHistoryExerciseDto>? Exercises { get; set; } = [];

        public DateTimeOffset? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}