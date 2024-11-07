using SharedKernel.Interfaces;
using System.Reflection;

namespace FormBuilderService.Host.DependencyContainer.Services.Abstraction
{
    public static class ServiceInjector
    {
        public static void InjectServices(this WebApplicationBuilder applicationBuilder)
        {
            var referencedAssemblies = FindReferencingAssemblies();

            var injectors = referencedAssemblies
                .SelectMany(assembly => assembly.ExportedTypes)
                .Where(serviceInjector =>
                    typeof(IServiceInjector).IsAssignableFrom(serviceInjector) && !serviceInjector.IsInterface && !serviceInjector.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IServiceInjector>()
                .ToList();


            injectors.ForEach(injector => injector.Inject(applicationBuilder.Services, applicationBuilder.Configuration));
        }

        private static IEnumerable<Assembly> FindReferencingAssemblies()
        {
            Type injectorInterface = typeof(IServiceInjector);
            var referencingAssemblies = new List<Assembly>();

            foreach (var assemblyFile in Directory.GetFiles(Path.GetDirectoryName(injectorInterface.Assembly.Location)!, "*.dll"))
            {
                try
                {
                    var assembly = Assembly.LoadFrom(assemblyFile);
                    var types = assembly.GetTypes();

                    foreach (var type in types)
                    {
                        var interfaces = type.GetInterfaces();

                        if (interfaces.Any(i => i == injectorInterface))
                        {
                            referencingAssemblies.Add(assembly!);

                            break;
                        }
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading assembly '{assemblyFile}': {ex.Message}");
                }
            }

            return referencingAssemblies;
        }
    }
}