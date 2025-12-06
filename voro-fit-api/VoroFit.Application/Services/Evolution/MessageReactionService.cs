using AutoMapper;
using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Interfaces.Repositories.Evolution;

namespace VoroFit.Application.Services.Evolution
{
    public class MessageReactionService(IMessageReactionRepository messageReactionRepository, IMapper mapper) : ServiceBase<MessageReaction>(messageReactionRepository), IMessageReactionService
    {
        public Task AddAsync(MessageReactionDto messageReactionDto)
        {
            var messageReaction = mapper.Map<MessageReaction>(messageReactionDto);

            return this.AddAsync(messageReaction);
        }

        public Task AddRangeAsync(IEnumerable<MessageReactionDto> messageReactionDtos)
        {
            var messageReactions = mapper.Map<IEnumerable<MessageReaction>>(messageReactionDtos);

            return this.AddRangeAsync(messageReactions);
        }
    }
}
