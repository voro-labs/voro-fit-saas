using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class StudentDto
    {
        public Guid UserExtensionId { get; set; }
        public UserExtensionDto UserExtension { get; set; } = null!;

        public Guid? TrainerId { get; set; }
        public UserExtensionDto? Trainer { get; set; }

        public int Age { get; set; }
        public int Height { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Weight { get; set; }

        public StudentStatusEnum Status { get; set; } 
            = StudentStatusEnum.Active;

        [StringLength(200)]
        public string Goal { get; set; } = string.Empty;

        // ===============================
        // RELACIONAMENTOS IMPORTANTES
        // ===============================

        // PLANOS (planejamento)
        public ICollection<WorkoutPlanDto> WorkoutPlans { get; set; } = [];
        public ICollection<MealPlanDto> MealPlans { get; set; } = [];

        // HISTÓRICOS (execução)
        public ICollection<WorkoutHistoryDto> WorkoutHistories { get; set; } = [];

        // Acompanhamento
        public ICollection<MeasurementDto> Measurements { get; set; } = [];

        // exercícios favoritos / frequentes
        public ICollection<ExerciseDto> FavoriteExercises { get; set; } = [];
    }
}
