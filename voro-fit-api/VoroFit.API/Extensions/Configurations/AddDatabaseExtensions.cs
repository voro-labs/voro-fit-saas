using VoroFit.Infrastructure.Factories;
using VoroFit.Shared.Utils;
using Microsoft.EntityFrameworkCore;

namespace VoroFit.API.Extensions.Configurations
{
    public static class AddDatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<JasmimDbContext>(options =>
                options.UseNpgsql(configuration.Get<ConfigUtil>()?.ConnectionDB));

            return services;
        }
    }
}
