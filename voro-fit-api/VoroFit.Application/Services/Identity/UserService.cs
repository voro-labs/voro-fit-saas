using VoroFit.Application.Services.Interfaces.Identity;
using VoroFit.Domain.Interfaces.Repositories.Identity;
using VoroFit.Domain.Entities.Identity;
using VoroFit.Application.Services.Base;

namespace VoroFit.Application.Services.Identity
{
    public class UserService(IUserRepository roleRepository) : ServiceBase<User>(roleRepository), IUserService
    {
        private readonly IUserRepository _roleRepository = roleRepository;
    }
}
