using AutoMapper;
using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Interfaces.Repositories.Evolution;

namespace VoroFit.Application.Services.Evolution
{
    public class MessageService(IMessageRepository messageRepository, IMapper mapper) : ServiceBase<Message>(messageRepository), IMessageService
    {
        public Task AddAsync(MessageDto messageDto)
        {
            var message = mapper.Map<Message>(messageDto);

            return base.AddAsync(message);
        }

        public Task AddRangeAsync(IEnumerable<MessageDto> messageDtos)
        {
            var messages = mapper.Map<IEnumerable<Message>>(messageDtos);

            return base.AddRangeAsync(messages);
        }
    }
}
