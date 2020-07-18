using Microsoft.Extensions.DependencyInjection;
using Moonlight.Extensions;
using NosCore.Packets.Interfaces;

namespace Moonlight.Tests.Utility
{
    public static class TestHelper
    {
        internal static IDeserializer CreateDeserializer()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddLogger();
            services.AddPacketDependencies();

            return services.BuildServiceProvider().GetService<IDeserializer>();
        }
    }
}