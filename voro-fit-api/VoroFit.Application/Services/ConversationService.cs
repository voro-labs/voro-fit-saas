using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Domain.Entities.Evolution;
using Microsoft.Extensions.Logging;
using VoroFit.Shared.Utils;
using Microsoft.Extensions.Options;
using VoroFit.Application.DTOs.Evolution.API.Request;

namespace VoroFit.Application.Services
{
    public class ConversationService(ILogger<ConversationService> logger, IChatService chatService,
        IGroupService groupService, IContactService contactService,
        IInstanceService instanceService, IGroupMemberService groupMemberService,
        IContactIdentifierService contactIdentifierService, IOptions<EvolutionUtil> evolutionUtil
    ) : IConversationService
    {
        private readonly ILogger<ConversationService> _logger = logger;
        private readonly IChatService _chatService = chatService;
        private readonly IGroupService _groupService = groupService;
        private readonly IContactService _contactService = contactService;
        private readonly IInstanceService _instanceService = instanceService;
        private readonly IGroupMemberService _groupMemberService = groupMemberService;
        private readonly IContactIdentifierService _contactIdentifierService = contactIdentifierService;
        private readonly EvolutionUtil _evolutionUtil = evolutionUtil.Value;

        public async Task<(Contact senderContact, Group? group, Chat chat)> CreateChatAndGroupOrContactAsync(
            string instanceName, string normalizedJid, string pushName,
            string remoteJid, bool isGroup = false, string? participant = "")
        {
            var instanceRequestDto = new InstanceRequestDto() { InstanceName = instanceName };

            instanceRequestDto.SetWebhookUrl(_evolutionUtil);

            var instance = await _instanceService.GetOrCreateInstance(instanceRequestDto);

            // Chat sempre vincula ao JID NORMALIZADO
            var chat = await _chatService.GetOrCreateChat(normalizedJid, instance, isGroup);

            // -------- CONTATO DO REMETENTE ---------

            ContactIdentifier contactIdentifier;
            Group? group = null;

            if (isGroup)
            {
                // Quem realmente enviou?
                // participant sempre vem com o JID de quem enviou dentro do grupo
                var participantJid = participant; // key.Participant;

                if (string.IsNullOrWhiteSpace(participantJid))
                    participantJid = normalizedJid; // fallback improvável

                // Normalizar participant (usar identificadores caso existam)
                var partIdentifier = await _contactService.FindByAnyAsync(participantJid);
                var normalizedParticipantJid = partIdentifier?.RemoteJid ?? participantJid;

                contactIdentifier = await _contactIdentifierService
                    .GetOrCreateAsync(pushName, normalizedParticipantJid, remoteJid, "");

                await _contactService.UpdateContact(
                    contactIdentifier.Contact,
                    pushName,
                    ""
                );

                // Criar grupo
                group = await _groupService.GetOrCreateGroup("Não Informado", normalizedJid);

                // Garantir que o contato é membro do grupo
                await _groupMemberService.EnsureGroupMembership(group, contactIdentifier.Contact);
            }
            else
            {
                // Mensagem direta
                var senderJid = normalizedJid;

                // Normalizar sender
                var sendIdentifier = await _contactService.FindByAnyAsync(senderJid);
                var normalizedSenderJid = senderJid;

                contactIdentifier = await _contactIdentifierService
                    .GetOrCreateAsync(pushName, normalizedSenderJid, remoteJid);

                chat.ContactId = contactIdentifier.Contact.Id;
            }

            return (contactIdentifier.Contact, group, chat);
        }
    }
}
