using FormBuilderService.Application.Commands.Abstraction;
using FormBuilderService.Application.Commands.Enums;
using FormBuilderService.Application.FactoryAbstraction;
using FormBuilderService.Domain.Interfaces.Factory;

namespace FormBuilderService.Application.Commands
{
    public class CommandFactory : GenericFactory<CommandType, Command>
    {
        public CommandFactory(IRepositoriesFactory repositoriesFactory) : base(repositoriesFactory)
        {
        }
    }
}