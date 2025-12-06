using VoroFit.Application.DTOs.Evolution.API;
using VoroFit.Application.DTOs.Request;
using VoroFit.Domain.Entities.Evolution;

namespace VoroFit.Application.Services.Interfaces.Evolution
{
    public interface IEvolutionService
    {
        Task<(Contact senderContact, Group? group, Chat chat)> CreateChatAndGroupOrContactAsync(
            string instanceName, string normalizedJid, string pushName,
            string remoteJid, bool isGroup = false, string? participant = "");

        Task<GroupEventDto?> GetGroupAsync(string groupJId);
        Task<IEnumerable<GroupEventDto>> GetGroupsAsync();
        Task<IEnumerable<ContactEventDto>> GetContactsAsync();
        Task<InstanceEventDto> GetInstanceStatusAsync();
        Task<string> SendMessageAsync(MessageRequestDto request);
        Task<string> SendMediaMessageAsync(MediaRequestDto request);
        Task<string> SendLocationMessageAsync(LocationRequestDto request);
        Task<string> SendReactionMessageAsync(ReactionRequestDto request);
        Task<string> SendContactMessageAsync(ContactRequestDto request);
        Task<string> SendQuotedMessageAsync(MessageRequestDto request);
        Task<string> DeleteMessageAsync(DeleteRequestDto request);
    }
}
