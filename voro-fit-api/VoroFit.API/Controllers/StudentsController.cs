using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VoroFit.API.Extensions;
using VoroFit.API.ViewModels;
using VoroFit.Application.DTOs;
using VoroFit.Application.DTOs.Identity;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Application.Services.Interfaces.Identity;
using VoroFit.Shared.Constants;
using VoroFit.Shared.Helpers;

namespace VoroFit.API.Controllers
{
    [Route("api/v{version:version}/[controller]")]
    [Tags("Students")]
    [ApiController]
    [Authorize]
    public class StudentsController(IStudentService studentService, IUserService userService) : ControllerBase
    {

        // ---------------------------------------------
        // GET /students
        // ---------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var result = await studentService.GetAllAsync();

                return ResponseViewModel<IEnumerable<StudentDto>>
                    .Success(result)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<IEnumerable<StudentDto>>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ---------------------------------------------
        // GET /students/{id}
        // ---------------------------------------------
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            try
            {
                var student = await studentService.GetByIdAsync(id);

                return ResponseViewModel<StudentDto>
                    .Success(student)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<StudentDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ---------------------------------------------
        // POST /students (com FormData)
        // ---------------------------------------------
        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDto model)
        {
            try
            {
                var userDto = new UserDto
                {
                    Email = $"{model.UserExtension?.User?.Email}",
                    FirstName = model.UserExtension?.User?.FirstName ?? string.Empty,
                    LastName = model.UserExtension?.User?.LastName ?? string.Empty,
                    AvatarUrl = model.UserExtension?.User?.AvatarUrl ?? string.Empty,
                    CountryCode = model.UserExtension?.User?.CountryCode ?? string.Empty,
                    PhoneNumber = model.UserExtension?.User?.PhoneNumber ?? string.Empty
                };

                var user = await userService
                    .CreateAsync(userDto, RandomTextHelper.GenerateRandomText, [RoleConstant.Student]) 
                        ?? throw new KeyNotFoundException("User não encontrado.");

                model.UserExtensionId = user.Id;
                model.UserExtension = null;

                var updated = await studentService.UpdateAsync(user.Id, model);

                await studentService.SaveChangesAsync();

                return ResponseViewModel<StudentDto>
                    .SuccessWithMessage("Aluno criado com sucesso.", updated)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<StudentDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ---------------------------------------------
        // PUT /students/{id}
        // ---------------------------------------------
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] StudentDto model)
        {
            try
            {
                var userDto = new UserDto
                {
                    Email = $"{model.UserExtension?.User?.Email}",
                    FirstName = model.UserExtension?.User?.FirstName ?? string.Empty,
                    LastName = model.UserExtension?.User?.LastName ?? string.Empty,
                    AvatarUrl = model.UserExtension?.User?.AvatarUrl ?? string.Empty,
                    CountryCode = model.UserExtension?.User?.CountryCode ?? string.Empty,
                    PhoneNumber = model.UserExtension?.User?.PhoneNumber ?? string.Empty
                };

                var user = await userService.UpdateAsync(id, userDto);

                model.UserExtensionId = user.Id;
                model.UserExtension = null;

                var updated = await studentService.UpdateAsync(id, model);

                await studentService.SaveChangesAsync();

                return ResponseViewModel<StudentDto>
                    .SuccessWithMessage("Aluno atualizado com sucesso.", updated)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<StudentDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ---------------------------------------------
        // DELETE /students/{id}
        // ---------------------------------------------
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            try
            {
                await studentService.DeleteAsync(id);

                await studentService.SaveChangesAsync();

                return ResponseViewModel<string>
                    .SuccessWithMessage("Aluno excluído com sucesso.", null)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<string>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ---------------------------------------------
        // POST /students/{id}/measurements
        // ---------------------------------------------
        [HttpPost("{id:guid}/measurements")]
        public async Task<IActionResult> AddMeasurement(Guid id, [FromBody] MeasurementDto model)
        {
            try
            {
                var measurement = await studentService.AddMeasurementAsync(id, model);

                return ResponseViewModel<MeasurementDto>
                    .Success(measurement)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<MeasurementDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }
    }
}
