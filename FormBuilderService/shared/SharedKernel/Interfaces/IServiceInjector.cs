using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SharedKernel.Interfaces
{
    public interface IServiceInjector
    {
        void Inject(IServiceCollection services, IConfiguration? configuration = default!);
    }
}