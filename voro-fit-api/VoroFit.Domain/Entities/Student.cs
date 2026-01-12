using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Enums;

namespace VoroFit.Domain.Entities
{
    public class Student
    {
        [Key]
        public Guid UserExtensionId { get; set; }
        public UserExtension UserExtension { get; set; } = null!;

        public Guid? TrainerId { get; set; }
        public UserExtension? Trainer { get; set; }

        public int Age { get; set; }
        public int Height { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Weight { get; set; }

        public StudentStatusEnum Status { get; set; } 
            = StudentStatusEnum.Active;

        [StringLength(200)]
        public string Goal { get; set; } = string.Empty;

        [StringLength(200)]
        public string Notes { get; set; } = string.Empty;

        // ===============================
        // RELACIONAMENTOS IMPORTANTES
        // ===============================

        // PLANOS (planejamento)
        public ICollection<WorkoutPlan> WorkoutPlans { get; set; } = [];
        public ICollection<MealPlan> MealPlans { get; set; } = [];

        // HISTÓRICOS (execução)
        public ICollection<WorkoutHistory> WorkoutHistories { get; set; } = [];

        // Acompanhamento
        public ICollection<Measurement> Measurements { get; set; } = [];

        // exercícios favoritos / frequentes
        public ICollection<Exercise> FavoriteExercises { get; set; } = [];
    }
}
