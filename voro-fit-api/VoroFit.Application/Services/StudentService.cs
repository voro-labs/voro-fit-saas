using VoroFit.Domain.Entities;
using VoroFit.Application.Services.Base;
using VoroFit.Domain.Interfaces.Repositories;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace VoroFit.Application.Services
{
    public class StudentService(IStudentRepository studentRepository, IMapper mapper) : ServiceBase<Student>(studentRepository), IStudentService
    {
        public Task<MeasurementDto> AddMeasurementAsync(Guid studentId, MeasurementDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<StudentDto> CreateAsync(StudentDto dto)
        {
            var createStudentDto = mapper.Map<Student>(dto);

            var student = base.AddAsync(createStudentDto);

            return mapper.Map<StudentDto>(student);
        }

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
                .Where(s => s.UserExtensionId == id)
                .FirstOrDefaultAsync();

            return mapper.Map<StudentDto?>(student);
        }

        public async Task<StudentDto> UpdateAsync(Guid id, StudentDto dto)
        {
            var updateStudentDto = mapper.Map<Student>(dto);

            var existingStudent = base.GetByIdAsync(id);

            if (existingStudent != null)
            {
                base.Update(updateStudentDto);
            }

            return mapper.Map<StudentDto>(updateStudentDto);
        }
    }
}
