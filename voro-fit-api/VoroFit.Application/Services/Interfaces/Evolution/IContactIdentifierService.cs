using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Entities.Identity;

namespace VoroFit.Application.Services.Interfaces.Evolution
{
    public interface IContactIdentifierService : IServiceBase<ContactIdentifier>
    {
        Task<ContactIdentifier> GetOrCreateAsync(string pushName, string remoteJid, string? remoteJidAlt, string? profilePicture = null, User? user = null);
        Task AddAsync(ContactIdentifierDto contactIdentifierDto);
    }
}
