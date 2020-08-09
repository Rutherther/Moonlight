using Moonlight.Remote.Client.State;

namespace Moonlight.Remote.Client
{
    public class RemoteClient : Clients.Client
    {
        private IState _state;

        public override void SendPacket(string packet)
        {
            _state.SendPacket(packet);
        }

        public override void ReceivePacket(string packet)
        {
            _state.ReceivePacket(packet);
        }

        public IState GetState()
        {
            return _state;
        }

        public void SetState(IState state)
        {
            _state = state;
            _state.PacketReceived += (packet) => OnPacketReceived(packet);
        }
    }
}
