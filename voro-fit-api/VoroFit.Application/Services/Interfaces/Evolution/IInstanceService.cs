using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities.Evolution;

namespace VoroFit.Application.Services.Interfaces.Evolution
{
    public interface IInstanceService : IServiceBase<Instance>
    {
        Task AddAsync(InstanceDto instanceDto);
        Task AddRangeAsync(IEnumerable<InstanceDto> instanceDtos);
        Task<Instance> GetOrCreateInstance(string name);
    }
}
