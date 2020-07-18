using Moonlight.Clients;
using NosCore.Packets.ServerPackets.Miniland;

namespace Moonlight.Handlers.Maps.Minilands.Minigames
{
    internal class MlPtPacketHandler : PacketHandler<MinilandPointPacket>
    {
        protected override void Handle(Client client, MinilandPointPacket packet)
        {
            if (client.Character != null)
            {
                client.Character.ProductionPoints = packet.MinilandPoint;
            }
        }
    }
}