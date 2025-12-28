using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.DTOs.Evolution.API.Request;
using VoroFit.Application.DTOs.Evolution.API.Response;
using VoroFit.Domain.Entities.Evolution;

namespace VoroFit.Application.Services.Interfaces
{
    public interface IConversationService
    {
        Task<(Contact senderContact, Group? group, Chat chat)> CreateChatAndGroupOrContactAsync(
            string instanceName, string normalizedJid, string pushName,
            string remoteJid, bool isGroup = false, string? participant = "");
    }
}
