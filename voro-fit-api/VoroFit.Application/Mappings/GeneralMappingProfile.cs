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
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Measurement, MeasurementDto>().ReverseMap();
            CreateMap<Exercise, ExerciseDto>().ReverseMap();
            CreateMap<WorkoutExercise, WorkoutExerciseDto>().ReverseMap();
            CreateMap<WorkoutHistory, WorkoutHistoryDto>().ReverseMap();
            CreateMap<MealPlan, MealPlanDto>().ReverseMap();
            CreateMap<MealPlanDay, MealPlanDayDto>().ReverseMap();
            CreateMap<MealPlanMeal, MealPlanMealDto>().ReverseMap();
        }
    }
}
