using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.DTOs.Evolution.API.Request;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Enums;
using VoroFit.Domain.Interfaces.Repositories.Evolution;

namespace VoroFit.Application.Services.Evolution
{
    public class InstanceService(
        IInstanceRepository instanceRepository,
        IMapper mapper,
        ICurrentUserService currentUserService,
        IEvolutionService evolutionService) : ServiceBase<Instance>(instanceRepository), IInstanceService
    {
        public Task AddAsync(InstanceDto instanceDto)
        {
            var instance = mapper.Map<Instance>(instanceDto);

            return base.AddAsync(instance);
        }

        public Task AddRangeAsync(IEnumerable<InstanceDto> instanceDtos)
        {
            var instances = mapper.Map<IEnumerable<Instance>>(instanceDtos);

            return base.AddRangeAsync(instances);
        }

        public async Task<Instance> GetOrCreateInstance(InstanceRequestDto instanceRequestDto, string? phoneNumber = null)
        {
            var name = instanceRequestDto.InstanceName.ToLower();

            var instance = await this
                .Query(i => i.Name.ToLower() == name.ToLower())
                .Include(i => i.InstanceExtension)
                .FirstOrDefaultAsync();

            if (instance != null)
                return instance;

            var response = await evolutionService.CreateInstanceAsync(instanceRequestDto);

            if (response == null)
                throw new Exception("Ocorreu um erro ao tentar salvar instancia nova.");

            instance = new Instance 
            {
                Id = response.Instance?.InstanceId ?? Guid.NewGuid(),
                Name = $"{response.Instance?.InstanceName}",
                UserExtensionId = currentUserService.UserId ?? Guid.Empty,
                InstanceExtension = new() 
                {
                    PhoneNumber = $"{phoneNumber}",
                    Hash = $"{response.Hash}",
                    Status = InstanceStatusEnum.Unspecified,
                    Base64 = $"{response.Qrcode?.Base64}",
                    Integration = $"{response.Instance?.Integration}"
                }
            };

            await base.AddAsync(instance);

            await base.SaveChangesAsync();

            return instance;
        }

        public async Task<Instance> UpdateStatus(Guid id, InstanceStatusEnum status)
        {
            var instance = await base.Query(i => i.Id == id)
                .Include(i => i.InstanceExtension)
                .FirstOrDefaultAsync();

            instance!.InstanceExtension!.Status = status;

            if (InstanceStatusEnum.Connected == status)
                instance.InstanceExtension!.ConnectedAt = DateTime.UtcNow;
            
            base.Update(instance);

            await base.SaveChangesAsync();

            return instance;
        }
    }
}
