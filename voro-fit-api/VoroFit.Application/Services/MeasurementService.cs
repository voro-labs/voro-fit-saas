using VoroFit.Domain.Entities;
using VoroFit.Application.Services.Base;
using VoroFit.Domain.Interfaces.Repositories;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace VoroFit.Application.Services
{
    public class MeasurementService(IMeasurementRepository measurementRepository, IMapper mapper) : ServiceBase<Measurement>(measurementRepository), IMeasurementService
    {
        public async Task<MeasurementDto> CreateAsync(MeasurementDto dto)
        {
            var measurement = mapper.Map<Measurement>(dto);

            measurement.StudentId = dto.StudentId!.Value;

            await base.AddAsync(measurement);

            return mapper.Map<MeasurementDto>(measurement);
        }

        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<MeasurementDto>> GetAllAsync()
        {
            var measurements = await base.Query()
                .Include(s => s.Student)
                    .ThenInclude(s => s.UserExtension)
                        .ThenInclude(s => s.User)
                .ToListAsync();

            return mapper.Map<IEnumerable<MeasurementDto>>(measurements);
        }

        public async Task<MeasurementDto?> GetByIdAsync(Guid id)
        {
            var measurement = await base.Query()
                .Include(s => s.Student)
                    .ThenInclude(s => s.UserExtension)
                        .ThenInclude(s => s.User)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            return mapper.Map<MeasurementDto?>(measurement);
        }

        public async Task<MeasurementDto?> GetByIdAsync(Guid id, Guid studentId)
        {
            var measurement = await base.Query()
                .Include(s => s.Student)
                    .ThenInclude(s => s.UserExtension)
                        .ThenInclude(s => s.User)
                .Where(s => s.Id == id && s.StudentId == studentId)
                .FirstOrDefaultAsync();

            return mapper.Map<MeasurementDto?>(measurement);
        }

        public async Task<MeasurementDto> UpdateAsync(Guid id, MeasurementDto dto)
        {
            var existingMeasurement = await base.GetByIdAsync(
                mp => mp.Id == id,
                mp => mp.Include(s => s.Student)
                        .ThenInclude(s => s.UserExtension)
                        .ThenInclude(s => s.User)
            ) ?? throw new Exception("Measurement não encontrado");

            mapper.Map(dto, existingMeasurement);

            existingMeasurement.UpdatedAt = DateTimeOffset.UtcNow;

            base.Update(existingMeasurement);

            return mapper.Map<MeasurementDto>(existingMeasurement);
        }
    }
}
