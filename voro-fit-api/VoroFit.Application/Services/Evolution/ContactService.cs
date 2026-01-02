using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VoroFit.Application.DTOs.Evolution;
using VoroFit.Application.Services.Base;
using VoroFit.Application.Services.Interfaces.Evolution;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Interfaces.Repositories.Evolution;

namespace VoroFit.Application.Services.Evolution
{
    public class ContactService(IContactRepository contactRepository, IMapper mapper) 
        : ServiceBase<Contact>(contactRepository), IContactService
    {
        public Task AddAsync(ContactDto contactDto)
        {
            var contact = mapper.Map<Contact>(contactDto);

            return base.AddAsync(contact);
        }

        public Task AddRangeAsync(IEnumerable<ContactDto> contactDtos)
        {
            var contacts = mapper.Map<IEnumerable<Contact>>(contactDtos);

            return base.AddRangeAsync(contacts);
        }

        public async Task<Contact?> FindByAnyAsync(string jid)
        {
            return await base.Query(c =>
                    c.RemoteJid == jid ||
                    c.Identifiers.Any(i => i.Jid == jid))
                .Include(c => c.Identifiers)
                .FirstOrDefaultAsync();
        }

        public Contact? UpdateContact(
            Contact contact,
            string? displayName,
            string? profilePicture)
        {
            if (contact != null)
            {
                if (!string.IsNullOrEmpty(displayName))
                    contact.DisplayName = displayName;

                if (!string.IsNullOrEmpty(profilePicture) &&
                    (string.IsNullOrEmpty(contact.ProfilePictureUrl) 
                    || contact.ProfilePictureUrl is not null && !contact.ProfilePictureUrl.StartsWith("data:")))
                    contact.ProfilePictureUrl = profilePicture;

                contact.UpdatedAt = DateTimeOffset.UtcNow;
                
                base.Update(contact);
            }


            return contact;
        }

        public async Task<Contact> GetOrCreateContact(
            string remoteJid,
            string displayName)
        {
            var contact = await this
                .Query(c => c.RemoteJid == remoteJid)
                .FirstOrDefaultAsync();

            // Já existe → retorna
            if (contact != null)
                return contact;

            contact = new Contact
            {
                RemoteJid = $"{remoteJid}",
                Number = remoteJid.Split("@")[0],
                DisplayName = displayName, // só salva se for válido
                UpdatedAt = DateTimeOffset.UtcNow
            };

            await base.AddAsync(contact);
            await base.SaveChangesAsync();

            return contact;
        }
    }
}
