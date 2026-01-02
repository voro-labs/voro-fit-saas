using VoroFit.Application.Mappings;
using VoroFit.Application.Mappings.Identity;
using VoroFit.Application.Mappings.Evolution;
using Microsoft.Extensions.DependencyInjection;

namespace VoroFit.Contract.Extensions.Configurations
{
    public static class AddMappingExtension
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<IdentityMappingProfile>();
                cfg.AddProfile<GeneralMappingProfile>();
                cfg.AddProfile<ContactMappingProfile>();
                cfg.AddProfile<GroupMappingProfile>();
                cfg.AddProfile<ReadMappingProfile>();
                cfg.AddProfile<WriteMappingProfile>();
            });

            return services;
        }
    }
}
