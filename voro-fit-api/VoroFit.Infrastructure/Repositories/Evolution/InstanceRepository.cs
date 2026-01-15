using VoroFit.Infrastructure.UnitOfWork;
using VoroFit.Infrastructure.Repositories.Base;
using VoroFit.Domain.Entities.Evolution;
using VoroFit.Domain.Interfaces.Repositories.Evolution;

namespace VoroFit.Infrastructure.Repositories
{
    public class InstanceRepository(IUnitOfWork unitOfWork) : RepositoryBase<Instance>(unitOfWork), IInstanceRepository
    {

    }
}
