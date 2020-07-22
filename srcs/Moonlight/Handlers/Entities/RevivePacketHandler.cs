using Moonlight.Clients;
using Moonlight.Game.Maps;
using Moonlight.Packet.Entity;

namespace Moonlight.Handlers.Entities
{
    public class RevivePacketHandler : PacketHandler<RevivePacket>
    {
        protected override void Handle(Client client, RevivePacket packet)
        {
            Map map = client.Character?.Map;
            map?.AddRemovedEntity(packet.EntityId);
        }
    }
}