using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities.Evolution;

namespace VoroFit.Application.Services.Interfaces.Evolution
{
    public interface IContactIdentifierService : IServiceBase<ContactIdentifier>
    {
        Task<ContactIdentifier> GetOrCreateAsync(string pushName, string remoteJid, string? remoteJidAlt, string? profilePicture = null);
        Task AddAsync(ContactIdentifierDto contactIdentifierDto);
    }
}
