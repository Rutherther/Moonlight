using Moonlight.Handlers;

namespace Moonlight.Clients
{
    public interface IClientManager
    {
        IPacketHandlerManager PacketHandlerManager { get; }
    }

    internal class ClientManager : IClientManager
    {
        public ClientManager(IPacketHandlerManager packetHandlerManager) => PacketHandlerManager = packetHandlerManager;

        public IPacketHandlerManager PacketHandlerManager { get; }
    }
}