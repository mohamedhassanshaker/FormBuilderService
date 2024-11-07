using FormBuilderService.Application.Interfaces.Abstraction;

namespace FormBuilderService.Application.Services.Factory
{
    public interface IServicesFactory
    {
        public TService GetService<TService>() where TService : IBaseService;
    }
}