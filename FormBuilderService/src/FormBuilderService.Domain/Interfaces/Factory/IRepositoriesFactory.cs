using FormBuilderService.Domain.Interfaces.Repositories.Abstraction;

namespace FormBuilderService.Domain.Interfaces.Factory
{
    public interface IRepositoriesFactory
    {
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        TRepository GetCustomRepository<TRepository, TEntity>()
            where TRepository : IBaseRepository<TEntity>
            where TEntity : class;
    }
}