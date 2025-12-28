using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.DTOs.Evolution.API.Request;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Enums;

namespace VoroFit.Application.Services.Interfaces.Evolution
{
    public interface IInstanceService : IServiceBase<Instance>
    {
        Task AddAsync(InstanceDto instanceDto);
        Task AddRangeAsync(IEnumerable<InstanceDto> instanceDtos);
        Task<Instance> GetOrCreateInstance(InstanceRequestDto instanceRequestDto);
        Task<Instance> UpdateStatus(Guid id, InstanceStatusEnum status);
    }
}
