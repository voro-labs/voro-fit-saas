using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Interfaces.Repositories.Evolution;

namespace VoroFit.Application.Services.Evolution
{
    public class ContactIdentifierService(IContactIdentifierRepository contactIdentifierRepository,
            IContactRepository contactRepository, IMapper mapper) 
        : ServiceBase<ContactIdentifier>(contactIdentifierRepository), IContactIdentifierService
    {
        public Task AddAsync(ContactIdentifierDto contactIdentifierDto)
        {
            var contactIdentifier = mapper.Map<ContactIdentifier>(contactIdentifierDto);

            return base.AddAsync(contactIdentifier);
        }

        // Cria ou retorna um Contact já existente
        public async Task<ContactIdentifier> GetOrCreateAsync(string pushName, string remoteJid, string? remoteJidAlt, string? profilePicture)
        {
            Contact? contact = null;

            // 1. Se já existe o primary
            var existingIdentifier = await GetIdentifierAsync(remoteJid);
            if (existingIdentifier != null)
            {
                contact = await contactRepository
                    .Include(c => c.Identifiers)
                    .FirstOrDefaultAsync(c => c.Id == existingIdentifier.ContactId);

                // Alt pode ser novo -> criar
                if (!string.IsNullOrWhiteSpace(remoteJidAlt))
                    await EnsureIdentifier(contact!, remoteJidAlt);

                return existingIdentifier;
            }

            // 2. Se o Alt já existe como identifier
            if (!string.IsNullOrWhiteSpace(remoteJidAlt))
            {
                var altIdentifier = await GetIdentifierAsync(remoteJidAlt);
                if (altIdentifier != null)
                {
                    contact = await contactRepository
                        .Include(c => c.Identifiers)
                        .FirstOrDefaultAsync(c => c.Id == altIdentifier.ContactId);

                    // Adicionar remoteJid como novo identifier
                    await EnsureIdentifier(contact!, remoteJid);
                    return altIdentifier;
                }
            }

            // 3. Nenhum existe → criar novo Contact
            contact = new Contact
            {
                DisplayName = pushName,
                RemoteJid = remoteJid,
                ProfilePictureUrl = profilePicture,
                Number = ExtractNumber(remoteJid)
            };

            await contactRepository.AddAsync(contact);

            await contactRepository.SaveChangesAsync();

            // Criar identifier principal
            List<ContactIdentifier> identPrimaries = [
                new()
                {
                    ContactId = contact.Id,
                    Jid = remoteJid,
                    IsPrimary = true
                }
            ];

            // Criar identifier alternativo se existir
            if (!string.IsNullOrWhiteSpace(remoteJidAlt))
            {
                identPrimaries.Add(
                    new ContactIdentifier
                    {
                        ContactId = contact.Id,
                        Jid = remoteJidAlt,
                        IsPrimary = false
                    }
                );
            }
            
            await base.AddRangeAsync(identPrimaries);

            await base.SaveChangesAsync();

            return identPrimaries
                .FirstOrDefault(item => item.IsPrimary)!;
        }

        private async Task<ContactIdentifier?> GetIdentifierAsync(string jid)
        {
            return await base.Query(ci => ci.Jid == jid)
                .Include(ci => ci.Contact)
                .FirstOrDefaultAsync();
        }

        // Garante a existência de um Identifier secundário
        private async Task EnsureIdentifier(Contact contact, string jid)
        {
            if (contact.Identifiers.Any(i => i.Jid == jid))
                return;

            var newIdent = new ContactIdentifier
            {
                ContactId = contact.Id,
                Jid = jid,
                IsPrimary = false
            };

            await base.AddAsync(newIdent);
            await base.SaveChangesAsync();
        }

        private string ExtractNumber(string jid)
        {
            return jid.Contains("@") ? jid.Split('@')[0] : jid;
        }
    }
}
