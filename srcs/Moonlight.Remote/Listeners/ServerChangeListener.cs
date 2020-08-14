using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moonlight.Event;
using Moonlight.Event.World;
using Moonlight.Remote.Client;
using Moonlight.Remote.Client.State;
using Moonlight.Remote.Control;

namespace Moonlight.Remote.Listeners
{
    public class ServerChangeListener : EventListener<ServerChangeEvent>
    {
        private NostaleWorld _world;
        private RemoteClient _client;
        
        public ServerChangeListener(NostaleWorld world, RemoteClient client)
        {
            _client = client;
            _world = world;
        }
        
        protected async override void Handle(ServerChangeEvent notification)
        {
            IState state = _client.GetState();
            if (state is RemoteClientWorldState worldState)
            {
                await Task.Delay(500);
                
                var newState = new RemoteClientWorldReconnectState(notification.DACIdentifier, notification.Ip, notification.Port, worldState.EncryptionKey);
                _client.SetState(newState);
                _world.SetRemoteState(newState);
                
                worldState.SendPacket("c_close 0");
                worldState.SendPacket("c_stash_end");
                worldState.SendPacket("c_close 1");
                worldState.Disconnnect();

                await Task.Delay(500);
                newState.Connect();
                await Task.Delay(100);
                newState.Handshake(_world.AccountName);
                
                Thread.Sleep(100);

                _world.StartGame();
            }
        }
    }
}