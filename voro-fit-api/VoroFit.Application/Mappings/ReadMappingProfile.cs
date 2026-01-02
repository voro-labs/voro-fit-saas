using AutoMapper;
using VoroFit.Application.DTOs;
using VoroFit.Domain.Entities;

namespace VoroFit.Application.Mappings
{
    public class ReadMappingProfile : Profile
    {
        public ReadMappingProfile()
        {
            CreateMap<UserExtension, UserExtensionDto>();
            CreateMap<Student, StudentDto>();
            CreateMap<Measurement, MeasurementDto>();
            CreateMap<Exercise, ExerciseDto>();

            CreateMap<WorkoutPlan, WorkoutPlanDto>();
            CreateMap<WorkoutPlanWeek, WorkoutPlanWeekDto>();
            CreateMap<WorkoutPlanDay, WorkoutPlanDayDto>();
            CreateMap<WorkoutPlanExercise, WorkoutPlanExerciseDto>();

            CreateMap<WorkoutHistory, WorkoutHistoryDto>();
            CreateMap<WorkoutHistoryExercise, WorkoutHistoryExerciseDto>();

            CreateMap<MealPlan, MealPlanDto>();
            CreateMap<MealPlanDay, MealPlanDayDto>();
            CreateMap<MealPlanMeal, MealPlanMealDto>();
        }
    }
}