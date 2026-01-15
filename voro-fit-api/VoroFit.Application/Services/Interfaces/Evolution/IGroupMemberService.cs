using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities.Evolution;

namespace VoroFit.Application.Services.Interfaces.Evolution
{
    public interface IGroupMemberService : IServiceBase<GroupMember>
    {
        Task EnsureGroupMembership(Group group, Contact contact);
    }
}
