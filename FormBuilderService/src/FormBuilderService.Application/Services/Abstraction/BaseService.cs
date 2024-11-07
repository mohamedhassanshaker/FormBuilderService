using FormBuilderService.Application.Interfaces.Abstraction;
using FormBuilderService.Domain.Interfaces.Factory;

namespace FormBuilderService.Application.Services.Abstraction
{
    public abstract class BaseService : IBaseService
    {
        private readonly IRepositoriesFactory _repositoriesFactory;

        public BaseService(IRepositoriesFactory repositoriesFactory)
        {
            _repositoriesFactory = repositoriesFactory;
        }
    }
}