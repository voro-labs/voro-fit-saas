using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VoroFit.Domain.Entities;
using VoroFit.Domain.Enums;

namespace VoroFit.Application.DTOs
{
    public class StudentDto
    {
        public Guid? UserExtensionId { get; set; } = null;
        public UserExtensionDto? UserExtension { get; set; } = null;

        public Guid? TrainerId { get; set; }
        public UserExtensionDto? Trainer { get; set; } = null;

        public int Age { get; set; }
        public int Height { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Weight { get; set; }

        public StudentStatusEnum Status { get; set; } = StudentStatusEnum.Active;

        [StringLength(200)]
        public string Goal { get; set; } = string.Empty;

        public ICollection<WorkoutExerciseDto> WorkoutExercises { get; set; } = [];
        public ICollection<WorkoutHistoryDto> WorkoutHistories { get; set; } = [];
        public ICollection<MeasurementDto> Measurements { get; set; } = [];
        public ICollection<MealPlanDto> MealPlans { get; set; } = [];
    }
}
