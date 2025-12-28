using Microsoft.EntityFrameworkCore;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Interfaces.Repositories.Evolution;

namespace VoroFit.Application.Services.Evolution
{
    public class ChatService(IChatRepository chatRepository) : ServiceBase<Chat>(chatRepository), IChatService
    {
        public async Task<Chat> GetOrCreateChat(string remoteJid, Instance instance, bool isGroup)
        {
            var chat = await this
                .Query(c => c.RemoteJid == remoteJid)
                .FirstOrDefaultAsync();

            if (chat != null)
                return chat;

            chat = new Chat
            {
                RemoteJid = remoteJid,
                IsGroup = isGroup,
                InstanceExtensionId = instance.Id,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            await base.AddAsync(chat);
            await base.SaveChangesAsync();

            return chat;
        }
    }
}
