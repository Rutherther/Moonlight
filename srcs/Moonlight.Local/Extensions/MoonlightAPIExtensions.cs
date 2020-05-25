using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moonlight.Clients;
using Moonlight.Local.Interop;

namespace Moonlight.Local.Extensions
{
    public static class MoonlightAPIExtensions
    {
        public static Client CreateLocalClient(this MoonlightAPI moonlightApi)
        {
            IClientManager clientManager = moonlightApi.Services.GetService<IClientManager>();
            return moonlightApi.Client = clientManager.CreateLocalClient();
        }

        public static void AllocConsole(this MoonlightAPI moonlightApi)
        {
            Kernel32.AllocConsole();
        }
    }
}
