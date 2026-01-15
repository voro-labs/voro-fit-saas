using VoroFit.Domain.Entities;
using VoroFit.Infrastructure.UnitOfWork;
using VoroFit.Domain.Interfaces.Repositories;
using VoroFit.Infrastructure.Repositories.Base;

namespace VoroFit.Infrastructure.Repositories
{
    public class WorkoutPlanExerciseRepository(IUnitOfWork unitOfWork) : RepositoryBase<WorkoutPlanExercise>(unitOfWork), IWorkoutPlanExerciseRepository
    {
    }
}
