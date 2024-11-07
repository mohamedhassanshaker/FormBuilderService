using FormBuilderService.Domain.Interfaces.Factory;
using FormBuilderService.Domain.Interfaces.Repositories.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Utilities.Logging.Services;

namespace FormBuilderService.Infrastructure.Persistence.Repositories.Factory
{
    public class RepositoriesFactory : IRepositoriesFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoriesFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            try
            {
                return _serviceProvider.GetService<IBaseRepository<TEntity>>()!;
            }

            catch (Exception ex)
            {
                LogService.LogError(ex);

                throw;
            }
        }

        public TRepository GetCustomRepository<TRepository, TEntity>()
            where TRepository : IBaseRepository<TEntity>
            where TEntity : class
        {
            try
            {
                return _serviceProvider.GetService<TRepository>()!;
            }

            catch (Exception ex)
            {
                LogService.LogError(ex);

                throw;
            }
        }
    }
}