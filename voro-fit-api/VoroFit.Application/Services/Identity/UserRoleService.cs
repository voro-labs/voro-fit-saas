using VoroFit.Application.Services.Interfaces.Identity;
using VoroFit.Domain.Interfaces.Repositories.Identity;
using VoroFit.Domain.Entities.Identity;
using VoroFit.Application.Services.Base;

namespace VoroFit.Application.Services.Identity
{
    public class UserRoleService(IUserRoleRepository userRoleRepository) : ServiceBase<UserRole>(userRoleRepository), IUserRoleService
    {
        
    }
}
