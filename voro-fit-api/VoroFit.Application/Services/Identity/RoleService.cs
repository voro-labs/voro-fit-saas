using VoroFit.Application.Services.Interfaces.Identity;
using VoroFit.Domain.Interfaces.Repositories.Identity;
using Microsoft.EntityFrameworkCore;
using VoroFit.Domain.Entities.Identity;
using VoroFit.Application.Services.Base;

namespace VoroFit.Application.Services.Identity
{
    public class RoleService(IRoleRepository roleRepository) : ServiceBase<Role>(roleRepository), IRoleService
    {
        public async Task<Role?> GetByNameAsync(string roleName)
            => await roleRepository.Query(r => r.Name == roleName).FirstOrDefaultAsync();
    }
}
