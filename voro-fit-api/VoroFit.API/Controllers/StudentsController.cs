using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoroFit.API.Extensions;
using VoroFit.API.ViewModels;
using VoroFit.Application.DTOs;
using VoroFit.Application.Services.Interfaces;

namespace VoroFit.API.Controllers
{
    [Route("api/v{version:version}/[controller]")]
    [Tags("Students")]
    [ApiController]
    [Authorize]
    public class StudentsController(IStudentService studentService) : ControllerBase
    {
        private readonly IStudentService _studentService = studentService;

        // ---------------------------------------------
        // GET /students
        // ---------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var result = await _studentService.GetAllAsync();

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
                var student = await _studentService.GetByIdAsync(id);

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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateStudent([FromForm] StudentDto model)
        {
            try
            {
                var created = await _studentService.CreateAsync(model);

                await _studentService.SaveChangesAsync();

                return ResponseViewModel<StudentDto>
                    .SuccessWithMessage("Aluno criado com sucesso.", created)
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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateStudent(Guid id, [FromForm] StudentDto model)
        {
            try
            {
                var updated = await _studentService.UpdateAsync(id, model);

                await _studentService.SaveChangesAsync();

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
                await _studentService.DeleteAsync(id);

                await _studentService.SaveChangesAsync();

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
                var measurement = await _studentService.AddMeasurementAsync(id, model);

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
