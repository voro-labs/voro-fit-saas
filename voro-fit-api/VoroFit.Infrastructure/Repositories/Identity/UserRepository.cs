using VoroFit.Domain.Entities.Identity;
using VoroFit.Domain.Interfaces.Repositories.Identity;
using VoroFit.Infrastructure.Repositories.Base;
using VoroFit.Infrastructure.UnitOfWork;

namespace VoroFit.Infrastructure.Repositories.Identity
{
    public class UserRepository(IUnitOfWork unitOfWork) : RepositoryBase<User>(unitOfWork), IUserRepository
    {
    }
}
