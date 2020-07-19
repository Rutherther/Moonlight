using System;

namespace Moonlight.Remote.Client.State
{
    public interface IState
    {
        event Action<string> PacketReceived;
        
        string IpAddress { get; }

        int Port { get; }

        void SendPacket(string packet, bool session = false);
        void ReceivePacket(string packet);
    }
}
