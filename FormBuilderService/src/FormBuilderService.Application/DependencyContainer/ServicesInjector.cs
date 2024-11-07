using FormBuilderService.Application.Interfaces.Abstraction;
using FormBuilderService.Application.Services.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Interfaces;
using System.Reflection;

namespace FormBuilderService.Application.DependencyContainer
{
    public class ServicesInjector : IServiceInjector
    {
        public void Inject(IServiceCollection services, IConfiguration? configuration = default!)
        {
            services.AddScoped<IServicesFactory, ServicesFactory>();

            RegisterServices(services, typeof(IBaseService));
        }

        private void RegisterServices(IServiceCollection services, Type baseType)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                                 .Where(t => IsService(t, baseType))
                                 .ToList();

            foreach (var type in types)
            {
                var interfaceType = GetServiceInterface(type, baseType);

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
            }
        }

        private bool IsService(Type type, Type baseType)
        {
            return type.GetInterfaces().Contains(baseType) && type.IsClass && !type.IsAbstract;
        }

        private Type GetServiceInterface(Type type, Type baseType)
        {
            return type.GetInterfaces().FirstOrDefault(i => i != baseType && baseType.IsAssignableFrom(i))!;
        }
    }
}