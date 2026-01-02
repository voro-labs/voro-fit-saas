using Microsoft.AspNetCore.Builder;
using VoroFit.Infrastructure.Seeds;
using VoroFit.Infrastructure.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace VoroFit.Shared.Extensions.Configurations
{
    public static class AddSeedExtension
    {
        public static async Task<IApplicationBuilder> UseSeedAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<JasmimDbContext>();
                
                var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
                
                await dataSeeder.SeedAsync(context);
            }

            return app;
        }
    }
}
