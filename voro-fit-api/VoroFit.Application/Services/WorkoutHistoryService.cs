using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Domain.Entities;
using VoroFit.Domain.Interfaces.Repositories;

namespace VoroFit.Application.Services
{
    public class WorkoutHistoryService(IWorkoutHistoryRepository workoutHistoryRepository, IMapper mapper) : ServiceBase<WorkoutHistory>(workoutHistoryRepository), IWorkoutHistoryService
    {
        public async Task<WorkoutHistoryDto> CreateAsync(WorkoutHistoryDto dto)
        {
            var createWorkoutHistoryDto = mapper.Map<WorkoutHistory>(dto);

            await base.AddAsync(createWorkoutHistoryDto);

            return mapper.Map<WorkoutHistoryDto>(createWorkoutHistoryDto);
        }

        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<WorkoutHistoryDto>> GetAllAsync()
        {
            var workoutHistories = await base.Query()
                .Include(s => s.Student)
                    .ThenInclude(s => s.UserExtension)
                        .ThenInclude(s => s.User)
                .Include(s => s.Exercises)
                .ToListAsync();

            return mapper.Map<IEnumerable<WorkoutHistoryDto>>(workoutHistories);
        }

        public async Task<WorkoutHistoryDto?> GetByIdAsync(Guid id)
        {
            var workoutHistory = await base.Query()
                .Include(s => s.Student)
                    .ThenInclude(s => s.UserExtension)
                        .ThenInclude(s => s.User)
                .Include(s => s.Exercises)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            return mapper.Map<WorkoutHistoryDto?>(workoutHistory);
        }

        public async Task<WorkoutHistoryDto> UpdateAsync(Guid id, WorkoutHistoryDto dto)
        {
            var existingWorkoutHistory = await base.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("WorkoutHistory não encontrado");
            
            mapper.Map(dto, existingWorkoutHistory);

            base.Update(existingWorkoutHistory);

            return mapper.Map<WorkoutHistoryDto>(existingWorkoutHistory);
        }
    }
}
