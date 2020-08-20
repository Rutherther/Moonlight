using System;

namespace Moonlight.Remote.Client.State
{
    public interface IState
    {
        event Action<string> PacketReceived;
        event Action<string> PacketSent;
        
        string IpAddress { get; }

        int Port { get; }

        void SendPacket(string packet, bool session);
        void SendPacket(string packet);
        void ReceivePacket(string packet);
    }
}
