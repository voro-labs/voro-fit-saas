using AutoMapper;
using VoroFit.Application.DTOs;
using VoroFit.Application.DTOs.Evolution;
using VoroFit.Domain.Entities;
using VoroFit.Domain.Entities.Evolution;

namespace VoroFit.Application.Mappings
{
    public class GeneralMappingProfile : Profile
    {
        public GeneralMappingProfile()
        {
            CreateMap<Chat, ChatDto>().ReverseMap();
            CreateMap<Contact, ContactDto>()
                .ForMember(
                    dest => dest.LastMessage,
                    opt => opt.MapFrom(src => src.LastMessage)
                ).ReverseMap();
            CreateMap<ContactIdentifierDto, ContactIdentifier>()
                .ForPath(dest => dest.Contact.RemoteJid, opt => opt.MapFrom(src => src.RemoteJidAlt))
                .ForMember(dest => dest.Jid, opt => opt.MapFrom(src => src.RemoteJid))
                .ReverseMap()
                .ForMember(dest => dest.RemoteJid, opt => opt.MapFrom(src => src.Jid))
                .ForMember(dest => dest.RemoteJidAlt, opt => opt.MapFrom(src => src.Contact.RemoteJid));
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<GroupMember, GroupMemberDto>().ReverseMap();
            CreateMap<Instance, InstanceDto>().ReverseMap();
            CreateMap<MessageReaction, MessageReactionDto>().ReverseMap();
            CreateMap<Message, MessageDto>().ReverseMap();
            
            CreateMap<InstanceExtension, InstanceExtensionDto>().ReverseMap();
            CreateMap<UserExtension, UserExtensionDto>().ReverseMap();

            // =========================
            // STUDENT
            // =========================
            CreateMap<Student, StudentDto>();

            CreateMap<StudentDto, Student>()
                .ForMember(d => d.UserExtensionId, o => o.Ignore())
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));
            
            // =========================
            // MEASUREMENT
            // =========================
            CreateMap<Measurement, MeasurementDto>();

            CreateMap<MeasurementDto, Measurement>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));

            // =========================
            // EXERCISE
            // =========================
            CreateMap<Exercise, ExerciseDto>();

            CreateMap<ExerciseDto, Exercise>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));

            // =========================
            // WORKOUT PLAN
            // =========================
            CreateMap<WorkoutPlan, WorkoutPlanDto>();

            CreateMap<WorkoutPlanDto, WorkoutPlan>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(x => x.Weeks, opt => opt.Ignore())
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<WorkoutPlanWeek, WorkoutPlanWeekDto>();

            CreateMap<WorkoutPlanWeekDto, WorkoutPlanWeek>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<WorkoutPlanDay, WorkoutPlanDayDto>();

            CreateMap<WorkoutPlanDayDto, WorkoutPlanDay>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<WorkoutPlanExercise, WorkoutPlanExerciseDto>();

            CreateMap<WorkoutPlanExerciseDto, WorkoutPlanExercise>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));

            // =========================
            // WORKOUT HISTORY
            // =========================
            CreateMap<WorkoutHistory, WorkoutHistoryDto>();

            CreateMap<WorkoutHistoryDto, WorkoutHistory>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<WorkoutHistoryExercise, WorkoutHistoryExerciseDto>();

            CreateMap<WorkoutHistoryExerciseDto, WorkoutHistoryExercise>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));

            // =========================
            // MEAL PLAN
            // =========================
            CreateMap<MealPlan, MealPlanDto>();

            CreateMap<MealPlanDto, MealPlan>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<MealPlanDay, MealPlanDayDto>();

            CreateMap<MealPlanDayDto, MealPlanDay>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<MealPlanMeal, MealPlanMealDto>();

            CreateMap<MealPlanMealDto, MealPlanMeal>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForAllMembers(o =>
                    o.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}