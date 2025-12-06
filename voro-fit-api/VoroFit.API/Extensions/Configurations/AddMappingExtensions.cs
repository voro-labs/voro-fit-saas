using VoroFit.Application.Mappings;
using VoroFit.Application.Mappings.Evolution;
using VoroFit.Application.Mappings.Identity;

namespace VoroFit.API.Extensions.Configurations
{
    public static class AddMappingExtensions
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<IdentityMappingProfile>();
                cfg.AddProfile<GeneralMappingProfile>();
                cfg.AddProfile<ContactMappingProfile>();
                cfg.AddProfile<GroupMappingProfile>();
            });

            return services;
        }
    }
}
