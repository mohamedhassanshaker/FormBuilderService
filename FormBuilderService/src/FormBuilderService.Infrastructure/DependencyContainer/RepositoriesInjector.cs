using FormBuilderService.Domain.Interfaces.Factory;
using FormBuilderService.Domain.Interfaces.Repositories.Abstraction;
using FormBuilderService.Infrastructure.Persistence.Repositories.Abstraction;
using FormBuilderService.Infrastructure.Persistence.Repositories.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Interfaces;
using System.Reflection;

namespace FormBuilderService.Infrastructure.DependencyContainer
{
    public class RepositoriesInjector : IServiceInjector
    {
        public void Inject(IServiceCollection services, IConfiguration? configuration = default!)
        {
            var baseType = typeof(IBaseRepository<>);

            services.AddScoped<IRepositoriesFactory, RepositoriesFactory>();

            services.AddScoped(baseType, typeof(BaseRepository<>));

            RegisterCustomRepositories(services, baseType);
        }

        private void RegisterCustomRepositories(IServiceCollection services, Type baseType)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                                 .Where(t => IsRepository(t, baseType))
                                 .ToList();

            foreach (var type in types)
            {
                var interfaceType = GetRepositoryInterface(type, baseType);

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
            }
        }

        private bool IsRepository(Type type, Type baseType)
        {
            return type.GetInterfaces().Length > 1 &&
                type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseType) &&
                type.IsClass && !type.IsAbstract;
        }

        private Type GetRepositoryInterface(Type type, Type baseType)
        {
            return type.GetInterfaces()
                .FirstOrDefault(it => !it.IsGenericType && it.GetInterfaces()
                    .Any(ii => ii.IsGenericType && ii.GetGenericTypeDefinition() == baseType))!;
        }
    }
}