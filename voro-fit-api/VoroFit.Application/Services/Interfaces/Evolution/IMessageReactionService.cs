using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities.Evolution;

namespace VoroFit.Application.Services.Interfaces.Evolution
{
    public interface IMessageReactionService : IServiceBase<MessageReaction>
    {
        Task AddAsync(MessageReactionDto messageReactionDto);
        Task AddRangeAsync(IEnumerable<MessageReactionDto> messageReactionDtos);
    }
}
