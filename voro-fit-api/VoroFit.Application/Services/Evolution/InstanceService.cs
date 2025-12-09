using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Interfaces.Repositories.Evolution;

namespace VoroFit.Application.Services.Evolution
{
    public class InstanceService(IInstanceRepository instanceRepository, IMapper mapper) : ServiceBase<Instance>(instanceRepository), IInstanceService
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

        public async Task<Instance> GetOrCreateInstance(string name)
        {
            name = name.ToLower();

            var instance = await this
                .Query(i => i.Name.ToLower() == name.ToLower())
                .FirstOrDefaultAsync();

            if (instance != null)
                return instance;

            instance = new Instance { Name = name };

            await base.AddAsync(instance);
            await base.SaveChangesAsync();

            return instance;
        }
    }
}
