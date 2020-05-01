using Moonlight.Clients;
using Moonlight.Packet;

namespace Moonlight.Handlers
{
    public interface IPacketHandler
    {
        void Handle(Client client, IPacket packet);
    }

    public abstract class PacketHandler<TPacket> : IPacketHandler where TPacket : IPacket
    {
        public void Handle(Client client, IPacket packet)
        {
            Handle(client, (TPacket)packet);
        }

        protected abstract void Handle(Client client, TPacket packet);
    }
}