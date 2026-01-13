using VoroFit.Domain.Entities;
using VoroFit.Application.Services.Base;
using VoroFit.Domain.Interfaces.Repositories;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Application.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VoroFit.Shared.Constants;
using VoroFit.Application.Services.Identity;

namespace VoroFit.Application.Services
{
    public class StudentService(IStudentRepository studentRepository, IMapper mapper) : ServiceBase<Student>(studentRepository), IStudentService
    {
        public Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var student = await base.Query()
                .Include(s => s.UserExtension)
                    .ThenInclude(s => s.User)
                .Include(s => s.WorkoutHistories)
                .Include(s => s.FavoriteExercises)
                .Include(s => s.WorkoutPlans)
                .Include(s => s.MealPlans)
                .Include(s => s.Measurements)
                .ToListAsync();

            return mapper.Map<IEnumerable<StudentDto>>(student);
        }

        public async Task<StudentDto?> GetByIdAsync(Guid id)
        {
            var student = await base.Query()
                .Include(s => s.UserExtension)
                    .ThenInclude(s => s.User)
                .Include(s => s.UserExtension)
                    .ThenInclude(s => s.Contact)
                        .ThenInclude(c => c!.Chats)
                .Include(s => s.WorkoutHistories)
                    .ThenInclude(wh => wh.WorkoutPlanDay)
                .Include(s => s.FavoriteExercises)
                .Include(s => s.WorkoutPlans)
                .Include(s => s.MealPlans)
                .Include(s => s.Measurements)
                .Where(s => s.UserExtensionId == id)
                .FirstOrDefaultAsync();

            return mapper.Map<StudentDto?>(student);
        }

        public async Task<StudentDto> UpdateAsync(Guid id, StudentDto dto)
        {
            var existingStudent = await base.GetByIdAsync(id)
                ?? throw new KeyNotFoundException("Student não encontrado");
            
            mapper.Map(dto, existingStudent);

            base.Update(existingStudent);

            return mapper.Map<StudentDto>(existingStudent);
        }
    }
}
