using VoroFit.Domain.Entities;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Application.DTOs;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IMeasurementService : IServiceBase<Measurement>
    {
        Task<IEnumerable<MeasurementDto>> GetAllAsync();
        Task<MeasurementDto?> GetByIdAsync(Guid id);
        Task<MeasurementDto?> GetByIdAsync(Guid id, Guid studentId);
        Task<MeasurementDto> CreateAsync(MeasurementDto model);
        Task<MeasurementDto> UpdateAsync(Guid id, MeasurementDto model);
        Task DeleteAsync(Guid id);
    }
}
