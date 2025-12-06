using VoroFit.Domain.Entities.Identity;
using VoroFit.Domain.Interfaces.Repositories.Identity;
using VoroFit.Infrastructure.Repositories.Base;
using VoroFit.Infrastructure.UnitOfWork;

namespace VoroFit.Infrastructure.Repositories.Identity
{
    public class UserRoleRepository(IUnitOfWork unitOfWork) : RepositoryBase<UserRole>(unitOfWork), IUserRoleRepository
    {

    }
}
