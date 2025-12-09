using VoroFit.Domain.Entities;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Interfaces.Base;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IStudentService : IServiceBase<Student>
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto?> GetByIdAsync(Guid id);
        Task<StudentDto> CreateAsync(StudentDto model);
        Task<StudentDto> UpdateAsync(Guid id, StudentDto model);
        Task DeleteAsync(Guid id);
        Task<MeasurementDto> AddMeasurementAsync(Guid studentId, MeasurementDto model);
    }
}
