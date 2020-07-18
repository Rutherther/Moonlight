using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Game.Maps;
using NosCore.Packets.ServerPackets.Portals;

namespace Moonlight.Handlers.Maps
{
    internal class GpPacketHandler : PacketHandler<GpPacket>
    {
        protected override void Handle(Client client, GpPacket packet)
        {
            Map map = client.Character?.Map;

            map?.AddPortal(new Portal(packet.PortalId, new Position(packet.SourceX, packet.SourceY), packet.MapId)
            {
                Type = packet.PortalType
            });
        }
    }
}