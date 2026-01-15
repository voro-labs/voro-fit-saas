using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities.Evolution;

namespace VoroFit.Application.Services.Interfaces.Evolution
{
    public interface IContactService : IServiceBase<Contact>
    {
        Task AddAsync(ContactDto contactDto);
        Task AddRangeAsync(IEnumerable<ContactDto> contactDtos);
        Task<Contact?> FindByAnyAsync(string jid);
        Contact? UpdateContact(Contact contact, string? displayName, string? profilePicture);
    }
}
