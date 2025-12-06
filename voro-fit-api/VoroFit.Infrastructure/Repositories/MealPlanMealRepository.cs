using VoroFit.Domain.Entities;
using VoroFit.Infrastructure.UnitOfWork;
using VoroFit.Domain.Interfaces.Repositories;
using VoroFit.Infrastructure.Repositories.Base;

namespace VoroFit.Infrastructure.Repositories
{
    public class MealPlanMealRepository(IUnitOfWork unitOfWork) : RepositoryBase<MealPlanMeal>(unitOfWork), IMealPlanMealRepository
    {
    }
}
