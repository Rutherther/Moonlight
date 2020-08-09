using System.Threading;
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
        
        protected override void Handle(ServerChangeEvent notification)
        {
            IState state = _client.GetState();
            if (state is RemoteClientWorldState worldState)
            {
                worldState.Disconnnect();
                var newState = new RemoteClientWorldReconnectState(notification.Ip, notification.Port, worldState.EncryptionKey);
                
                newState.Connect();
                newState.Handshake(_world.AccountName);
                
                _client.SetState(newState);
                Thread.Sleep(100);

                _world.SetRemoteState(newState);
                _world.StartGame();
            }
        }
    }
}