using VoroFit.Infrastructure.Factories;
using VoroFit.Infrastructure.Seeds;

namespace VoroFit.API.Extensions.Configurations
{
    public static class AddSeedExtensions
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
