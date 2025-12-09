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

            var exercise = base.AddAsync(createWorkoutHistoryDto);

            return mapper.Map<WorkoutHistoryDto>(exercise);
        }

        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<WorkoutHistoryDto>> GetAllAsync()
        {
            return await workoutHistoryRepository.Query()
                .Include(s => s.Student)
                    .ThenInclude(s => s.UserExtension)
                        .ThenInclude(s => s.User)
                .Include(s => s.Exercises)
                .ProjectTo<WorkoutHistoryDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<WorkoutHistoryDto?> GetByIdAsync(Guid id)
        {
            return await workoutHistoryRepository.Query()
                .Include(s => s.Student)
                    .ThenInclude(s => s.UserExtension)
                        .ThenInclude(s => s.User)
                .Include(s => s.Exercises)
                .Where(s => s.Id == id)
                .ProjectTo<WorkoutHistoryDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<WorkoutHistoryDto> UpdateAsync(Guid id, WorkoutHistoryDto dto)
        {
            var updateWorkoutHistoryDto = mapper.Map<WorkoutHistory>(dto);

            var existingWorkoutHistory = base.GetByIdAsync(id);

            if (existingWorkoutHistory != null)
            {
                base.Update(updateWorkoutHistoryDto);
            }

            return mapper.Map<WorkoutHistoryDto>(updateWorkoutHistoryDto);
        }
    }
}
