using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moonlight.Core.Logging;
using Moonlight.Event;
using Moonlight.Event.World;
using Moonlight.Remote.Client;
using Moonlight.Remote.Client.State;
using Moonlight.Remote.Control;

namespace Moonlight.Remote.Listeners
{
    public class ServerChangeListener : EventListener<ServerChangeEvent>
    {
        private readonly NostaleWorld _world;
        private readonly RemoteClient _client;
        private readonly ILogger _logger;
        
        public ServerChangeListener(ILogger logger, NostaleWorld world, RemoteClient client)
        {
            _logger = logger;
            _client = client;
            _world = world;
        }
        
        protected async override void Handle(ServerChangeEvent notification)
        {
            _logger.Debug($"Server change to {notification.Ip}:{notification.Port} event received");
            IState state = _client.GetState();
            if (state is RemoteClientWorldState worldState)
            {
                await Task.Delay(500).ConfigureAwait(false);
                
                var newState = new RemoteClientWorldReconnectState(_logger, _client.Region, notification.DACIdentifier, notification.Ip, notification.Port, worldState.EncryptionKey);
                _client.SetState(newState);
                _world.SetRemoteState(newState);
                
                worldState.SendPacket("c_close 0");
                worldState.SendPacket("c_stash_end");
                worldState.SendPacket("c_close 1");
                worldState.Disconnnect();

                await Task.Delay(500).ConfigureAwait(false);
                newState.Connect();
                await Task.Delay(100).ConfigureAwait(false);
                newState.Handshake(_world.AccountName);
                
                Thread.Sleep(100);

                _world.StartGame();
            }
        }
    }
}