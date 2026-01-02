using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using VoroFit.Infrastructure.Factories;
using VoroFit.Shared.Utils;

namespace VoroFit.Shared.Extensions.Configurations
{
    public static class AddDatabaseExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JasmimDbContext>(options =>
                options.UseNpgsql(configuration.Get<ConfigUtil>()?.ConnectionDB));

            return services;
        }
    }
}
