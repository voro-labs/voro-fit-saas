using AutoMapper;
using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.DTOs.Evolution.API.Response;

namespace VoroFit.Application.Mappings.Evolution
{

    public class GroupMappingProfile : Profile
    {
        public GroupMappingProfile()
        {
            CreateMap<GroupResponseDto, GroupDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))

            // id → RemoteJid
            .ForMember(dest => dest.RemoteJid, opt => opt.MapFrom(src => src.Id))

            // subject → Name
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject))

            // pictureUrl → ProfilePictureUrl
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.PictureUrl))

            // creation(timestamp unix) → CreatedAt
            .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.Creation)))

            // coleções
            .ForMember(dest => dest.Members, opt => opt.Ignore());
        }
    }
}
