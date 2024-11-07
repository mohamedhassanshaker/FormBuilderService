using FormBuilderService.Domain.Interfaces.Factory;

namespace FormBuilderService.Application.Queries.Abstraction
{
    public abstract class Query
    {
        private readonly IRepositoriesFactory _repositoryFactory;

        protected Query(IRepositoriesFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public abstract TReturn Handle<TReturn, TParam>(TParam parameter);
    }
}