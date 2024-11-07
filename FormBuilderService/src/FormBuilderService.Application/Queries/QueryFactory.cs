using FormBuilderService.Application.FactoryAbstraction;
using FormBuilderService.Application.Queries.Abstraction;
using FormBuilderService.Application.Queries.Enums;
using FormBuilderService.Domain.Interfaces.Factory;

namespace FormBuilderService.Application.Queries
{
    public class QueryFactory : GenericFactory<QueryType, Query>
    {
        public QueryFactory(IRepositoriesFactory repositoriesFactory) : base(repositoriesFactory)
        {
        }
    }
}