using AutoMapper;
using VoroFit.Application.DTOs;
using VoroFit.Domain.Entities;

namespace VoroFit.Application.Mappings
{
    public class WriteMappingProfile : Profile
    {
        public WriteMappingProfile()
        {
            CreateMap<UserExtensionDto, UserExtension>()
                .ForMember(d => d.UserId, o => o.Ignore());

            CreateMap<StudentDto, Student>()
                .ForMember(d => d.UserExtensionId, o => o.Ignore());

            CreateMap<MeasurementDto, Measurement>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.DeletedAt, o => o.Ignore());

            CreateMap<ExerciseDto, Exercise>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.WorkoutPlanExercises, o => o.Ignore())
                .ForMember(d => d.WorkoutHistoryExercises, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.DeletedAt, o => o.Ignore());

            CreateMap<WorkoutPlanDto, WorkoutPlan>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Weeks, o => o.Ignore())
                .ForMember(d => d.Student, o => o.Ignore())
                .ForMember(d => d.WorkoutHistories, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.DeletedAt, o => o.Ignore());

            CreateMap<WorkoutPlanWeekDto, WorkoutPlanWeek>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Days, o => o.Ignore())
                .ForMember(d => d.WorkoutPlan, o => o.Ignore())
                .ForMember(d => d.WorkoutHistories, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.DeletedAt, o => o.Ignore());

            CreateMap<WorkoutPlanDayDto, WorkoutPlanDay>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.WorkoutPlanWeek, o => o.Ignore())
                .ForMember(d => d.WorkoutHistories, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.DeletedAt, o => o.Ignore());

            CreateMap<WorkoutPlanExerciseDto, WorkoutPlanExercise>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Exercise, o => o.Ignore())
                .ForMember(d => d.WorkoutPlanDay, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.DeletedAt, o => o.Ignore());

            CreateMap<WorkoutHistoryDto, WorkoutHistory>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Student, o => o.Ignore())
                .ForMember(d => d.WorkoutPlanDay, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.DeletedAt, o => o.Ignore());

            CreateMap<WorkoutHistoryExerciseDto, WorkoutHistoryExercise>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Exercise, o => o.Ignore())
                .ForMember(d => d.WorkoutHistory, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.DeletedAt, o => o.Ignore());

            CreateMap<MealPlanDto, MealPlan>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Days, o => o.Ignore())
                .ForMember(d => d.Student, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.DeletedAt, o => o.Ignore());

            CreateMap<MealPlanDayDto, MealPlanDay>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Meals, o => o.Ignore())
                .ForMember(d => d.MealPlan, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.DeletedAt, o => o.Ignore());

            CreateMap<MealPlanMealDto, MealPlanMeal>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.MealPlanDay, o => o.Ignore())
                .ForMember(d => d.CreatedAt, o => o.Ignore())
                .ForMember(d => d.IsDeleted, o => o.Ignore())
                .ForMember(d => d.DeletedAt, o => o.Ignore());
        }
    }
}