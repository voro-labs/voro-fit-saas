using Microsoft.EntityFrameworkCore;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Interfaces.Repositories.Evolution;

namespace VoroFit.Application.Services.Evolution
{
    public class GroupMemberService(IGroupMemberRepository groupMemberRepository) : 
        ServiceBase<GroupMember>(groupMemberRepository), IGroupMemberService
    {
        public async Task EnsureGroupMembership(Group group, Contact contact)
        {
            var exists = await this
                .Query(m => m.GroupId == group.Id && m.ContactId == contact.Id)
                .AnyAsync();

            if (exists)
                return;

            await base.AddAsync(new GroupMember
            {
                GroupId = group.Id,
                ContactId = contact.Id,
                JoinedAt = DateTimeOffset.UtcNow
            });

            await base.SaveChangesAsync();
        }

    }
}
