using VoroFit.Application.DTOs;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDataDto> GetAllAsync();
    }
}
