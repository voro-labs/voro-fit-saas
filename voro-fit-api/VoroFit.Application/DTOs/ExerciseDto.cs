using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class ExerciseDto
    {
        public Guid? Id { get; set; }

        // Dados principais
        public string? Name { get; set; } = null!;
        public string? MuscleGroup { get; set; } = null!;
        public ExerciseTypeEnum? Type { get; set; }

        public string? Thumbnail { get; set; } = null!;

        // Conteúdo educacional
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public string? Alternatives { get; set; }

        // Relacionamentos
        public ICollection<WorkoutPlanExerciseDto>? WorkoutPlanExercises { get; set; } = [];
        public ICollection<WorkoutHistoryExerciseDto>? WorkoutHistoryExercises { get; set; } = [];

        // Auditoria
        public DateTimeOffset? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}