using SharedKernel.Common;
using SharedKernel.Interfaces;

namespace FormBuilderService.Host.DependencyContainer.Services
{
    public class InternalServicesInjector : IServiceInjector
    {
        public void Inject(IServiceCollection services, IConfiguration? configuration = default!)
        {
            var appSettingsSection = configuration?.GetSection("AppSettings");

            if (appSettingsSection?.Get<AppSettings>() is not null)
            {
                services.Configure<AppSettings>(appSettingsSection);

                AppSettings applicationSettings = appSettingsSection.Get<AppSettings>()!;

                services.Add(new ServiceDescriptor(typeof(AppSettings), applicationSettings));
            }

            services.ConfigureHttpJsonOptions((opt) =>
            {
                opt.SerializerOptions.PropertyNameCaseInsensitive = true;
                opt.SerializerOptions.PropertyNamingPolicy = null;
            });

            services.AddMemoryCache();
        }
    }
}