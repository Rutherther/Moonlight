using Moonlight.Clients;
using Moonlight.Game.Maps;
using NosCore.Packets.ServerPackets.Miniland;

namespace Moonlight.Handlers.Maps.Minilands
{
    internal class MlInfoPacketHandler : PacketHandler<MlinfoPacket>
    {
        protected override void Handle(Client client, MlinfoPacket packet)
        {
            client.Character.ProductionPoints = packet.MinilandPoint;
            
            var miniland = client.Character?.Map as Miniland;
            if (miniland == null)
            {
                return;
            }

            miniland.Owner = client.Character.Name;
        }
    }
}