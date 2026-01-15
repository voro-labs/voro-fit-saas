using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Application.DTOs.Evolution;

namespace VoroFit.Application.Services.Interfaces.Evolution
{
    public interface IGroupService : IServiceBase<Group>
    {
        Task AddAsync(GroupDto entity);
        Task AddRangeAsync(IEnumerable<GroupDto> entities);
        Task<Group> GetOrCreateGroup(string groupJid, string? displayName);
    }
}
