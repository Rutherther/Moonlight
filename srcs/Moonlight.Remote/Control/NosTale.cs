using Microsoft.Extensions.DependencyInjection;
using Moonlight.Core.Enums;
using Moonlight.Handlers;
using Moonlight.Remote.Client;

namespace Moonlight.Remote.Control
{
    public class NosTale
    {
        private readonly MoonlightAPI _moonlightApi;
        
        public NosTale(MoonlightAPI moonlightApi, RegionType region)
        {
            Region = region;
            _moonlightApi = moonlightApi;
        }
        
        public RegionType Region { get; set; }

        public RemoteClient GetRemoteClient()
        {
            var client = _moonlightApi.Client as RemoteClient;

            if (client == null)
            {
                _moonlightApi.Client = client = new RemoteClient(Region);

                client.PacketReceived += packet => _moonlightApi.Services.GetService<IPacketHandlerManager>().Handle(client, packet);
                client.PacketSend += packet => _moonlightApi.Services.GetService<IPacketHandlerManager>().Handle(client, packet);
            }
            
            return client;
        }

        public NosTaleLogin InitLogin()
        {
            return new NosTaleLogin( _moonlightApi, GetRemoteClient());
        }
    }
}
