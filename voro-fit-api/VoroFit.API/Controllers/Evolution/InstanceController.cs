using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VoroFit.Shared.Extensions;
using VoroFit.Shared.ViewModels;
using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.DTOs.Evolution.API.Request;
using VoroFit.Application.DTOs.Evolution.API.Response;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Domain.Enums;
using VoroFit.Shared.Utils;
using VoroFit.Application.Services.Interfaces;
using System.Net;

namespace VoroFit.API.Controllers.Evolution
{
    [Route("api/v{version:version}/[controller]")]
    [Tags("Evolution")]
    [ApiController]
    [Authorize]
    public class InstanceController(IMapper mapper, ICurrentUserService currentUserService, IInstanceService instanceService,
        IEvolutionService evolutionService, IOptions<EvolutionUtil> evolutionUtil) : ControllerBase
    {

        // ✅ GET api/v1/instance
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var instances = await instanceService.Query(i => i.InstanceExtension != null &&
                        i.UserExtensionId == currentUserService.UserId)
                    .Include(i => i.InstanceExtension).ToListAsync();

                var instancesDto = mapper.Map<IEnumerable<InstanceDto>>(instances);

                return ResponseViewModel<IEnumerable<InstanceDto>>
                    .SuccessWithMessage("Instances loaded successfully.", instancesDto)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<IEnumerable<InstanceDto>>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ✅ POST api/v1/instance
        [HttpPost("{phoneNumber}")]
        public async Task<IActionResult> Create(string phoneNumber, [FromBody] InstanceRequestDto dto)
        {
            try
            {
                dto.SetWebhookUrl(evolutionUtil.Value);

                var instance = await instanceService.GetOrCreateInstance(dto, phoneNumber);

                var instanceDto = mapper.Map<InstanceDto>(instance);

                return ResponseViewModel<InstanceDto>
                    .SuccessWithMessage("Instance created successfully.", instanceDto)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<InstanceDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ✅ DELETE api/v1/instance/{instanceName}
        [HttpDelete("{instanceName}")]
        public async Task<IActionResult> Delete(string instanceName)
        {
            try
            {
                evolutionService.SetInstanceName(instanceName);

                try
                {
                    await evolutionService.DeleteInstanceAsync();
                }
                catch(HttpRequestException ex)
                {
                    if (ex.StatusCode != HttpStatusCode.NotFound)
                    {
                        throw;
                    }
                }

                var instanceRequestDto = new InstanceRequestDto() { InstanceName = instanceName };

                instanceRequestDto.SetWebhookUrl(evolutionUtil.Value);

                var instance = await instanceService.GetOrCreateInstance(instanceRequestDto);

                await instanceService.DeleteAsync(instance.Id);

                await instanceService.SaveChangesAsync();

                return ResponseViewModel<object>
                    .SuccessWithMessage("Instance deleted successfully.", null)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<object>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ✅ GET api/v1/instance/{instanceName}/qrcode
        [HttpGet("{instanceName}/qrcode")]
        public async Task<IActionResult> RefreshQrCode(string instanceName)
        {
            try
            {
                evolutionService.SetInstanceName(instanceName);

                var instance = await evolutionService.RefreshQrCodeAsync();

                return ResponseViewModel<QrCodeJsonDto>
                    .SuccessWithMessage("QR Code refreshed successfully.", instance)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<QrCodeJsonDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }

        // ✅ GET api/v1/instance/{instanceName}/status (já existia)
        [HttpGet("{instanceName}/status")]
        public async Task<IActionResult> GetStatus(string instanceName)
        {
            try
            {
                evolutionService.SetInstanceName(instanceName);

                var response = await evolutionService.GetInstanceStatusAsync();

                var status = response.Instance!.State switch
                {
                    "open" => (InstanceStatusEnum?)InstanceStatusEnum.Connected,
                    "connecting" => (InstanceStatusEnum?)InstanceStatusEnum.Connecting,
                    _ => (InstanceStatusEnum?)InstanceStatusEnum.Disconnected,
                };

                var instanceRequestDto = new InstanceRequestDto() { InstanceName = instanceName };

                instanceRequestDto.SetWebhookUrl(evolutionUtil.Value);

                var instance = await instanceService.GetOrCreateInstance(instanceRequestDto);
                
                var instanceDto = mapper.Map<InstanceDto>(instance);
                
                if (instanceDto == null)
                    throw new Exception("Instance not found!");

                await instanceService.UpdateStatus(instanceDto.Id ?? Guid.Empty, status ?? InstanceStatusEnum.Unspecified);

                await instanceService.SaveChangesAsync();

                return ResponseViewModel<InstanceDto>
                    .SuccessWithMessage("Status successful.", instanceDto)
                    .ToActionResult();
            }
            catch (Exception ex)
            {
                return ResponseViewModel<InstanceDto>
                    .Fail(ex.Message)
                    .ToActionResult();
            }
        }
    }
}
