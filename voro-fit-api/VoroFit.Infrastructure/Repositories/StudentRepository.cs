using VoroFit.Domain.Entities;
using VoroFit.Infrastructure.UnitOfWork;
using VoroFit.Domain.Interfaces.Repositories;
using VoroFit.Infrastructure.Repositories.Base;

namespace VoroFit.Infrastructure.Repositories
{
    public class StudentRepository(IUnitOfWork unitOfWork) : RepositoryBase<Student>(unitOfWork), IStudentRepository
    {
    }
}
