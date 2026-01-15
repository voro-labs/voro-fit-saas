using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Entities.Identity;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IConversationService
    {
        Task<(Contact senderContact, Group? group, Chat chat)> CreateChatAndGroupOrContactAsync(
            string instanceName, string normalizedJid, string pushName,
            string remoteJid, bool isGroup = false, string? participant = "");

        Task<(Contact senderContact, Chat chat)> CreateChatAndGroupOrContactAsync(
            string instanceName, string normalizedJid, string remoteJid, User user);
    }
}
