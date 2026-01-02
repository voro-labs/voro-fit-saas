using Microsoft.AspNetCore.Identity;
using VoroFit.Infrastructure.Factories;
using VoroFit.Domain.Entities.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace VoroFit.Shared.Extensions.Configurations
{
    public static class AddIdentityExtension
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<JasmimDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
