using VoroFit.Domain.Enums;
using VoroFit.Domain.Interfaces.Entities;

namespace VoroFit.Domain.Entities
{
    public class Exercise : ISoftDeletable
    {
        public Guid Id { get; set; }

        // Dados principais
        public string Name { get; set; } = null!;
        public string MuscleGroup { get; set; } = null!;
        public ExerciseTypeEnum Type { get; set; }

        public string Thumbnail { get; set; } = null!;

        // Conteúdo educacional
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public string? Alternatives { get; set; }

        // Relacionamentos
        public ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; set; } = [];
        public ICollection<WorkoutHistoryExercise> WorkoutHistoryExercises { get; set; } = [];

        // Auditoria
        public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
