using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities.Evolution;

namespace VoroFit.Application.Services.Interfaces.Evolution
{
    public interface IChatService : IServiceBase<Chat>
    {
        Task<Chat> GetOrCreateChat(string remoteJid, Instance instance, bool isGroup);
    }
}
