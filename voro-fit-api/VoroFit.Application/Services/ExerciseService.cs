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

            var exercise = base.AddAsync(createExerciseDto);

            return mapper.Map<ExerciseDto>(exercise);
        }

        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllAsync()
        {
            return await exerciseRepository.Query()
                .Include(s => s.WorkoutExercises)
                .ProjectTo<ExerciseDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ExerciseDto?> GetByIdAsync(Guid id)
        {
            return await exerciseRepository.Query()
                .Where(s => s.Id == id)
                .ProjectTo<ExerciseDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<ExerciseDto> UpdateAsync(Guid id, ExerciseDto dto)
        {
            var updateExerciseDto = mapper.Map<Exercise>(dto);

            var existingExercise = base.GetByIdAsync(id);

            if (existingExercise != null)
            {
                base.Update(updateExerciseDto);
            }

            return mapper.Map<ExerciseDto>(updateExerciseDto);
        }
    }
}
