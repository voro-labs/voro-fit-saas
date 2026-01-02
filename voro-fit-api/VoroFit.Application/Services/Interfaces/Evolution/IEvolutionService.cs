using VoroFit.Application.DTOs.Evolution.API.Request;
using VoroFit.Application.DTOs.Evolution.API.Response;

namespace VoroFit.Application.Services.Interfaces.Evolution
{
    public interface IEvolutionService
    {
        void SetInstanceName(string instanceName);

        Task<IEnumerable<InstanceResponseDto>> GetAllInstancesAsync();
        Task<InstanceCreateResponseDto> CreateInstanceAsync(InstanceRequestDto dto);
        Task DeleteInstanceAsync();
        Task<QrCodeJsonDto> RefreshQrCodeAsync();
        Task<InstanceCreateResponseDto> GetInstanceStatusAsync();

        Task<GroupResponseDto?> GetGroupAsync(string groupJId);
        Task<IEnumerable<GroupResponseDto>> GetGroupsAsync();
        Task<IEnumerable<ContactResponseDto>> GetContactsAsync();
        Task<string> SendMessageAsync(MessageRequestDto request);
        Task<string> SendMediaMessageAsync(MediaRequestDto request);
        Task<string> SendLocationMessageAsync(LocationRequestDto request);
        Task<string> SendReactionMessageAsync(ReactionRequestDto request);
        Task<string> SendContactMessageAsync(ContactRequestDto request);
        Task<string> SendQuotedMessageAsync(MessageRequestDto request);
        Task<string> DeleteMessageAsync(DeleteRequestDto request);
    }
}
