using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moonlight.Clients;
using Moonlight.Local.Clients;
using Moonlight.Local.Clients.Local;
using Moonlight.Local.Interop;

namespace Moonlight.Local.Extensions
{
    public static class ClientManagerExtensions
    {
        public static Client CreateLocalClient(this IClientManager clientManager)
        {
            IEnumerable<IntPtr> windows = User32.FindWindowsWithTitle("NosTale");
            IntPtr currentWindow = windows.FirstOrDefault(x =>
            {
                User32.GetWindowThreadProcessId(x, out uint pid);
                return Process.GetCurrentProcess().Id == pid;
            });

            if (currentWindow == IntPtr.Zero)
            {
                throw new InvalidOperationException("Can't find window");
            }

            Client client = new LocalClient(new Window(currentWindow));

            client.PacketReceived += x => clientManager.PacketHandlerManager.Handle(client, x);
            client.PacketSend += x => clientManager.PacketHandlerManager.Handle(client, x);


            client.Walk += (s, e) => { (client as LocalClient).ManagedMoonlightCore.Walk(e.X, e.Y); };

            return client;
        }
    }
}
