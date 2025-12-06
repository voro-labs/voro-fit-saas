using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities.Evolution;

namespace VoroFit.Application.Services.Interfaces.Evolution
{
    public interface IMessageService : IServiceBase<Message>
    {
        Task AddAsync(MessageDto messageDto);
        Task AddRangeAsync(IEnumerable<MessageDto> messageDtos);
    }
}
