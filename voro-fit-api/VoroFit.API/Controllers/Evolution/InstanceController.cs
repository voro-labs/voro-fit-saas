using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VoroFit.API.Extensions;
using VoroFit.API.ViewModels;
using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.DTOs.Evolution.API.Request;
using VoroFit.Application.DTOs.Evolution.API.Response;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Domain.Enums;
using VoroFit.Shared.Utils;

namespace VoroFit.API.Controllers.Evolution
{
    [Route("api/v{version:version}/[controller]")]
    [Tags("Evolution")]
    [ApiController]
    [Authorize]
    public class InstanceController(IMapper mapper, IInstanceService instanceService, IEvolutionService evolutionService, IOptions<EvolutionUtil> evolutionUtil) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly IInstanceService _instanceService = instanceService;
        private readonly IEvolutionService _evolutionService = evolutionService;
        private readonly EvolutionUtil _evolutionUtil = evolutionUtil.Value;

        // ✅ GET api/v1/instance
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var instances = await _instanceService.Query()
                    .Include(i => i.InstanceExtension).ToListAsync();

                var instancesDto = _mapper.Map<IEnumerable<InstanceDto>>(instances);

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
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InstanceRequestDto dto)
        {
            try
            {
                dto.SetWebhookUrl(_evolutionUtil);

                var instance = await _instanceService.GetOrCreateInstance(dto);

                var instanceDto = _mapper.Map<InstanceDto>(instance);

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
                await _evolutionService.SetInstanceName(instanceName);

                await _evolutionService.DeleteInstanceAsync();

                var instanceRequestDto = new InstanceRequestDto() { InstanceName = instanceName };

                instanceRequestDto.SetWebhookUrl(_evolutionUtil);

                var instance = await _instanceService.GetOrCreateInstance(instanceRequestDto);

                await _instanceService.DeleteAsync(instance.Id);

                await _instanceService.SaveChangesAsync();

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
                await _evolutionService.SetInstanceName(instanceName);

                var instance = await _evolutionService.RefreshQrCodeAsync();

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
                await _evolutionService.SetInstanceName(instanceName);

                var response = await _evolutionService.GetInstanceStatusAsync();

                var status = response.Instance!.State switch
                {
                    "open" => (InstanceStatusEnum?)InstanceStatusEnum.Connected,
                    "connecting" => (InstanceStatusEnum?)InstanceStatusEnum.Connecting,
                    _ => (InstanceStatusEnum?)InstanceStatusEnum.Disconnected,
                };

                var instanceRequestDto = new InstanceRequestDto() { InstanceName = instanceName };

                instanceRequestDto.SetWebhookUrl(_evolutionUtil);

                var instance = await _instanceService.GetOrCreateInstance(instanceRequestDto);
                
                var instanceDto = _mapper.Map<InstanceDto>(instance);
                
                if (instanceDto == null)
                    throw new Exception("Instance not found!");

                await _instanceService.UpdateStatus(instanceDto.Id ?? Guid.Empty, status ?? InstanceStatusEnum.Unspecified);

                await _instanceService.SaveChangesAsync();

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
