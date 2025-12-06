using AutoMapper;
using VoroFit.Application.DTOs.Identity;
using VoroFit.Domain.Entities.Identity;

namespace VoroFit.Application.Mappings.Identity
{
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserRole, UserRoleDto>().ReverseMap();
        }
    }
}
