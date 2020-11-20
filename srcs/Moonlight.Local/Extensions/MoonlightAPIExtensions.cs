using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moonlight.Clients;
using Moonlight.Local.Clients;
using Moonlight.Local.Interop;

namespace Moonlight.Local.Extensions
{
    public static class MoonlightAPIExtensions
    {
        public static Client CreateLocalClient(this MoonlightAPI moonlightApi)
        {
            if (LocalClient.NonSharedInstanceCreated)
            {
                throw new InvalidOperationException("There is already one instance of local client created for non shared context. Packet or walk functions cannot be hooked.");
            }

            if (LocalClient.SharedInstanceCreated && !moonlightApi.SharedInstance)
            {
                throw new InvalidOperationException("There is already one instance of local client created for shared context. Packet or walk functions cannot be hooked. Use MoonlightAPI.GetSharedMoonlightAPI for more injected dlls at once.");
            }
            
            if (!moonlightApi.SharedInstance)
            {
                LocalClient.NonSharedInstanceCreated = true;
            }
            else
            {
                LocalClient.SharedInstanceCreated = true;

                if (moonlightApi.Client is LocalClient)
                {
                    return moonlightApi.Client;
                }
            }
            
            IClientManager clientManager = moonlightApi.Services.GetService<IClientManager>();
            return moonlightApi.Client = clientManager.CreateLocalClient();
        }

        public static void AllocConsole(this MoonlightAPI moonlightApi)
        {
            Kernel32.AllocConsole();
        }
    }
}
