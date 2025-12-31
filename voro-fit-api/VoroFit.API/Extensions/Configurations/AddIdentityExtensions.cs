using VoroFit.Domain.Entities.Identity;
using VoroFit.Infrastructure.Factories;
using Microsoft.AspNetCore.Identity;
using VoroFit.Domain.Entities;

namespace VoroFit.API.Extensions.Configurations
{
    public static class AddIdentityExtensions
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
