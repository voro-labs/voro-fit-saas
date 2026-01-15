using VoroFit.Infrastructure.UnitOfWork;
using VoroFit.Infrastructure.Repositories.Base;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Interfaces.Repositories.Evolution;

namespace VoroFit.Infrastructure.Repositories
{
    public class ContactRepository(IUnitOfWork unitOfWork) : RepositoryBase<Contact>(unitOfWork), IContactRepository
    {

    }
}
