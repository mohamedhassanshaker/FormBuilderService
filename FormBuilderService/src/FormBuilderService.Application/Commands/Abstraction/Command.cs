using FormBuilderService.Domain.Interfaces.Factory;

namespace FormBuilderService.Application.Commands.Abstraction
{
    public abstract class Command
    {
        private readonly IRepositoriesFactory _repositoryFactory;

        public Command(IRepositoriesFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public abstract TReturn Handle<TReturn, TParam>(TParam parameter);
        public abstract void Undo<TParam>(TParam parameter);
    }
}