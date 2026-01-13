using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoroFit.Shared.Extensions;
using VoroFit.Shared.ViewModels;
using VoroFit.Application.DTOs;
using VoroFit.Application.DTOs.Identity;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Application.Services.Interfaces.Identity;
using VoroFit.Shared.Constants;
using VoroFit.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using VoroFit.Application.Services.Interfaces.Evolution;

namespace VoroFit.API.Controllers
{
    [Route("api/v{version:version}/[controller]")]
    [Tags("Students")]
    [ApiController]
    [Authorize]
    public class StudentsController(IConversationService conversationService,
        IContactService contactService, IChatService chatService,
        IStudentService studentService, IMeasurementService measurementService, IUserService userService) : ControllerBase
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
                var user = await userService
                    .CreateAsync(model, RandomTextHelper.GenerateRandomText, [RoleConstant.Student]) 
                        ?? throw new KeyNotFoundException("User não encontrado.");

                var trainer = await userService.GetByIdAsync(
                    t => t.Id == model.TrainerId,
                    t => t.Include(t => t.UserExtension)
                        .ThenInclude(ue => ue!.Instances));

                var instances = trainer?.UserExtension?.Instances ?? [];

                foreach (var instance in instances)
                {
                    var countryCode = $"{user.CountryCode}".Replace("+", "");

                    var (senderContact, chat) = await conversationService.CreateChatAndGroupOrContactAsync(
                        $"{instance.Name}", $"{countryCode}{user.PhoneNumber}@s.whatsapp.net",
                        $"{countryCode}{user.PhoneNumber}@s.whatsapp.net", user);

                    if (senderContact != null)
                    {
                        senderContact.UserExtensionId = user.Id;
                        contactService.Update(senderContact);
                    }

                    chatService.Update(chat);

                    await contactService.SaveChangesAsync();
                    await chatService.SaveChangesAsync();
                }

                return ResponseViewModel<StudentDto>
                    .SuccessWithMessage("Aluno criado com sucesso.", model)
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
                await userService.SaveChangesAsync();

                model.UserExtensionId = user.Id;
                model.UserExtension = null;

                var updated = await studentService.UpdateAsync(id, model);
                await studentService.SaveChangesAsync();

                var trainer = await userService.GetByIdAsync(
                    t => t.Id == model.TrainerId,
                    t => t.Include(t => t.UserExtension)
                        .ThenInclude(ue => ue!.Instances));

                var instances = trainer?.UserExtension?.Instances ?? [];

                foreach (var instance in instances)
                {
                    var countryCode = $"{user.CountryCode}".Replace("+", "");

                    var (senderContact, chat) = await conversationService.CreateChatAndGroupOrContactAsync(
                        $"{instance.Name}", $"{countryCode}{user.PhoneNumber}@s.whatsapp.net",
                        $"{countryCode}{user.PhoneNumber}@s.whatsapp.net", user);

                    if (senderContact != null)
                    {
                        senderContact.UserExtensionId = user.Id;
                        contactService.Update(senderContact);
                    }

                    chatService.Update(chat);

                    await contactService.SaveChangesAsync();
                    await chatService.SaveChangesAsync();
                }

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

        // GET /api/v1/students/{id}/measurements/{measurementId}
        [HttpGet("{id:guid}/measurements/{measurementId:guid}")]
        public async Task<IActionResult> GetById(Guid id, Guid measurementId)
        {
            try
            {
                var result = await measurementService
                    .GetByIdAsync(measurementId, id);

                if (result is null)
                {
                    return ResponseViewModel<MeasurementDto>
                        .Fail("Medição não encontrada.")
                        .ToActionResult();
                }

                return ResponseViewModel<MeasurementDto>
                    .SuccessWithMessage("Medição carregada com sucesso.", result)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<MeasurementDto>
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
                if (id != model.StudentId)
                    return ResponseViewModel<MeasurementDto>
                    .Fail("Usuario não condiz com o codigo informado!")
                    .ToActionResult();

                var measurement = await measurementService.CreateAsync(model);

                await measurementService.SaveChangesAsync();

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
