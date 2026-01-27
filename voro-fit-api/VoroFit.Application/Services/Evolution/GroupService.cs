using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Interfaces.Repositories.Evolution;

namespace VoroFit.Application.Services.Evolution
{
    public class GroupService(IGroupRepository groupRepository, IMapper mapper) : ServiceBase<Group>(groupRepository), IGroupService
    {
        public Task AddAsync(GroupDto groupDto)
        {
            var group = mapper.Map<Group>(groupDto);

            return base.AddAsync(group);
        }

        public Task AddRangeAsync(IEnumerable<GroupDto> groupDtos)
        {
            var groups = mapper.Map<IEnumerable<Group>>(groupDtos);

            return base.AddRangeAsync(groups);
        }

        public async Task<Group?> GetAsync(string groupJid)
        {
            var group = await this
                .Query(g => g.RemoteJid == groupJid)
                .FirstOrDefaultAsync();

            if (group != null)
                return group;

            return null;
        }

        public async Task<Group> GetOrCreateAsync(string groupJid, string? displayName)
        {
            var group = await GetAsync(groupJid);

            if (group != null)
                return group;

            group = new Group
            {
                Name = displayName ?? "Desconhecido",
                RemoteJid = groupJid
            };

            await base.AddAsync(group);
            await base.SaveChangesAsync();

            return group;
        }
    }
}
