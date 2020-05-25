using Microsoft.Extensions.DependencyInjection;

namespace Moonlight.Core
{
    public interface IServiceConfiguration
    {
        void ConfigureServices(MoonlightAPI api, IServiceCollection services);
    }
}
