using VoroFit.Application.Services.Interfaces.Base;
using VoroFit.Domain.Entities.Identity;

namespace VoroFit.Application.Services.Interfaces.Identity
{
    public interface IRoleService : IServiceBase<Role>
    {
        Task<Role?> GetByNameAsync(string roleName);
    }
}
