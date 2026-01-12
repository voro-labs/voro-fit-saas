using AutoMapper;
using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.DTOs.Evolution.API.Response;

namespace VoroFit.Application.Mappings
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<ContactResponseDto, ContactDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))

            // remoteJid → RemoteJid
            .ForMember(dest => dest.RemoteJid, opt => opt.MapFrom(src => src.RemoteJid))

            // extrair número antes do @ 
            .ForMember(dest => dest.Number,
                opt => opt.MapFrom(src => ExtractNumber(src.RemoteJid, "")))

            // pushName → DisplayName
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.PushName))

            // profilePicUrl → ProfilePictureUrl
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePicUrl))

            // updatedAt → UpdatedAt
            .ForMember(dest => dest.UpdatedAt,
                opt => opt.MapFrom(src => ParseDate(src.UpdatedAt)))

            // presença inicial indefinida
            .ForMember(dest => dest.LastKnownPresence, opt => opt.MapFrom(src => ""))
            .ForMember(dest => dest.LastPresenceAt,
                opt => opt.MapFrom(_ => DateTimeOffset.UtcNow))

            // coleções vazias
            .ForMember(dest => dest.GroupMemberships, opt => opt.Ignore());
        }

        public static string ExtractNumber(string? remoteJid, string? remoteJidAlt)
        {
            if (string.IsNullOrWhiteSpace(remoteJid))
                return string.Empty;

            if (string.IsNullOrWhiteSpace(remoteJidAlt))
                return string.Empty;

            if (remoteJid.Contains("whatsapp"))
            {
                var beforeAt = remoteJid.Split('@').FirstOrDefault();
                if (string.IsNullOrWhiteSpace(beforeAt))
                    return string.Empty;

                return beforeAt.Split('-').FirstOrDefault() ?? string.Empty;
            }

            if (remoteJidAlt.Contains("whatsapp"))
            {
                var beforeAt = remoteJidAlt.Split('@').FirstOrDefault();
                if (string.IsNullOrWhiteSpace(beforeAt))
                    return string.Empty;

                return beforeAt.Split('-').FirstOrDefault() ?? string.Empty;
            }

            return string.Empty;
        }

        private static DateTimeOffset ParseDate(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return DateTimeOffset.UtcNow;

            if (long.TryParse(date, out var unix))
                return DateTimeOffset.FromUnixTimeSeconds(unix);

            if (DateTimeOffset.TryParse(date, out var dto))
                return dto;

            return DateTimeOffset.UtcNow;
        }
    }
}
