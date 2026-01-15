using VoroFit.Infrastructure.Factories;

namespace VoroFit.Infrastructure.Seeds
{
    public interface IDataSeeder
    {
        Task SeedAsync(JasmimDbContext context);
    }
}