using VoroFit.Domain.Interfaces.Repositories.Identity;
using VoroFit.Infrastructure.UnitOfWork;
using VoroFit.Domain.Entities.Identity;
using VoroFit.Infrastructure.Repositories.Base;

namespace VoroFit.Infrastructure.Repositories.Identity
{
    public class RoleRepository(IUnitOfWork unitOfWork) : RepositoryBase<Role>(unitOfWork), IRoleRepository
    {
       
    }
}
