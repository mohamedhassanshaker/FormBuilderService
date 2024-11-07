using FormBuilderService.Application.Interfaces.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Utilities.Logging.Services;

namespace FormBuilderService.Application.Services.Factory
{
    public class ServicesFactory : IServicesFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServicesFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TService GetService<TService>() where TService : IBaseService
        {
            try
            {
                return _serviceProvider.GetService<TService>()!;
            }
            catch (Exception ex)
            {
                LogService.LogError(ex);

                throw;
            }
        }
    }
}