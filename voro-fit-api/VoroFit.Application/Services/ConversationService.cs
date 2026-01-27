using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Application.Services.Interfaces;
using VoroFit.Domain.Entities.Evolution;
using Microsoft.Extensions.Logging;
using VoroFit.Shared.Utils;
using Microsoft.Extensions.Options;
using VoroFit.Application.DTOs.Evolution.API.Request;
using VoroFit.Domain.Entities.Identity;

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

        public async Task<(Contact senderContact, Group? group, Chat chat)> GetChatAndGroupOrContactAsync(
            string instanceName, string normalizedJid, string pushName,
            string remoteJid, bool isGroup = false, string? participant = "")
        {
            var instanceRequestDto = new InstanceRequestDto() { InstanceName = instanceName };

            instanceRequestDto.SetWebhookUrl(_evolutionUtil);

            var instance = await _instanceService.GetAsync(instanceRequestDto);

            // Chat sempre vincula ao JID NORMALIZADO
            var chat = await _chatService.GetAsync(normalizedJid)
                ?? throw new Exception("Chat não encontrado!");

            // -------- CONTATO DO REMETENTE ---------

            ContactIdentifier? contactIdentifier = null;
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

                contactIdentifier = await _contactIdentifierService.GetAsync(normalizedParticipantJid, remoteJid)
                    ?? throw new Exception("Contato não encontrado!");

                _contactService.UpdateContact(
                    contactIdentifier.Contact,
                    pushName,
                    ""
                );

                // Criar grupo
                group = await _groupService.GetAsync(normalizedJid)
                    ?? throw new Exception("Grupo não encontrado!");

                // Garantir que o contato é membro do grupo
                await _groupMemberService.EnsureGroupMembership(group, contactIdentifier.Contact);
            }
            else
            {
                // Mensagem direta
                var senderJid = normalizedJid;

                // Normalizar sender
                var sendIdentifier = await _contactService.FindByAnyAsync(senderJid);
                var normalizedSenderJid = sendIdentifier?.RemoteJid ?? senderJid;

                contactIdentifier = await _contactIdentifierService.GetAsync(normalizedSenderJid, remoteJid)
                    ?? throw new Exception("Contato não encontrado!");

                chat.ContactId = contactIdentifier.Contact.Id;
            }

            var contact = await _contactService.GetByIdAsync(contactIdentifier.ContactId) 
                ?? throw new Exception("Contato não encontrado!");
                
            return (contact, group, chat);
        }

        public async Task<(Contact senderContact, Chat chat)> GetChatAndGroupOrContactAsync(
            string instanceName, string normalizedJid, string remoteJid)
        {
            var instanceRequestDto = new InstanceRequestDto() { InstanceName = instanceName };

            instanceRequestDto.SetWebhookUrl(_evolutionUtil);

            var instance = await _instanceService.GetAsync(instanceRequestDto);

            // Chat sempre vincula ao JID NORMALIZADO
            var chat = await _chatService.GetAsync(normalizedJid)
                ?? throw new Exception("Chat não encontrado!");

            // -------- CONTATO DO REMETENTE ---------

            ContactIdentifier contactIdentifier;

            // Mensagem direta
            var senderJid = normalizedJid;

            // Normalizar sender
            var sendIdentifier = await _contactService.FindByAnyAsync(senderJid);
            var normalizedSenderJid = sendIdentifier?.RemoteJid ?? senderJid;

            contactIdentifier = await _contactIdentifierService.GetAsync(normalizedSenderJid, remoteJid)
                ?? throw new Exception("Contato não encontrado!");

            chat.ContactId = contactIdentifier.Contact.Id;

            var contact = await _contactService.GetByIdAsync(contactIdentifier.ContactId) 
                ?? throw new Exception("Contato não encontrado!");
                
            return (contact, chat);
        }

        public async Task<(Contact senderContact, Group? group, Chat chat)> CreateChatAndGroupOrContactAsync(
            string instanceName, string normalizedJid, string pushName,
            string remoteJid, bool isGroup = false, string? participant = "")
        {
            var instanceRequestDto = new InstanceRequestDto() { InstanceName = instanceName };

            instanceRequestDto.SetWebhookUrl(_evolutionUtil);

            var instance = await _instanceService.GetOrCreateAsync(instanceRequestDto);

            // Chat sempre vincula ao JID NORMALIZADO
            var chat = await _chatService.GetOrCreateAsync(normalizedJid, instance, isGroup);

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

                _contactService.UpdateContact(
                    contactIdentifier.Contact,
                    pushName,
                    ""
                );

                // Criar grupo
                group = await _groupService.GetOrCreateAsync("Não Informado", normalizedJid);

                // Garantir que o contato é membro do grupo
                await _groupMemberService.EnsureGroupMembership(group, contactIdentifier.Contact);
            }
            else
            {
                // Mensagem direta
                var senderJid = normalizedJid;

                // Normalizar sender
                var sendIdentifier = await _contactService.FindByAnyAsync(senderJid);
                var normalizedSenderJid = sendIdentifier?.RemoteJid ?? senderJid;

                contactIdentifier = await _contactIdentifierService
                    .GetOrCreateAsync(pushName, normalizedSenderJid, remoteJid, "");

                chat.ContactId = contactIdentifier.Contact.Id;
            }

            var contact = await _contactService.GetByIdAsync(contactIdentifier.ContactId) 
                ?? throw new Exception("Contato não encontrado!");
                
            return (contact, group, chat);
        }

        public async Task<(Contact senderContact, Chat chat)> CreateChatAndGroupOrContactAsync(
            string instanceName, string normalizedJid, string remoteJid, User user)
        {
            var instanceRequestDto = new InstanceRequestDto() { InstanceName = instanceName };

            instanceRequestDto.SetWebhookUrl(_evolutionUtil);

            var instance = await _instanceService.GetOrCreateAsync(instanceRequestDto);

            // Chat sempre vincula ao JID NORMALIZADO
            var chat = await _chatService.GetOrCreateAsync(normalizedJid, instance, false);

            // -------- CONTATO DO REMETENTE ---------

            ContactIdentifier contactIdentifier;

            // Mensagem direta
            var senderJid = normalizedJid;

            // Normalizar sender
            var sendIdentifier = await _contactService.FindByAnyAsync(senderJid);
            var normalizedSenderJid = sendIdentifier?.RemoteJid ?? senderJid;

            contactIdentifier = await _contactIdentifierService
                .GetOrCreateAsync($"{user.FirstName} {user.LastName}", normalizedSenderJid, remoteJid, user.AvatarUrl, user);

            chat.ContactId = contactIdentifier.Contact.Id;

            var contact = await _contactService.GetByIdAsync(contactIdentifier.ContactId) 
                ?? throw new Exception("Contato não encontrado!");
                
            return (contact, chat);
        }
    }
}
