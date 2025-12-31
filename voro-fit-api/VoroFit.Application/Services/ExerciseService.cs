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
    public class ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper) : ServiceBase<Exercise>(exerciseRepository), IExerciseService
    {
        public async Task<ExerciseDto> CreateAsync(ExerciseDto dto)
        {
            var createExerciseDto = mapper.Map<Exercise>(dto);

            await base.AddAsync(createExerciseDto);

            return mapper.Map<ExerciseDto>(createExerciseDto);
        }

        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllAsync()
        {
            var exercises = await base.Query()
                .Include(s => s.WorkoutPlanExercises)
                .Include(s => s.WorkoutHistoryExercises)
                .ToListAsync();

            return mapper.Map<IEnumerable<ExerciseDto>>(exercises);
        }

        public async Task<ExerciseDto?> GetByIdAsync(Guid id)
        {
            var exercise = await base.Query()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            return mapper.Map<ExerciseDto?>(exercise);
        }

        public async Task<ExerciseDto> UpdateAsync(Guid id, ExerciseDto dto)
        {
            var existingExercise = await base.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Exercise não encontrado");
            
            mapper.Map(dto, existingExercise);

            base.Update(existingExercise);

            return mapper.Map<ExerciseDto>(existingExercise);
        }
    }
}
