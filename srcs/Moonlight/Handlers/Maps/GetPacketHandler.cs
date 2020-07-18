using Moonlight.Clients;
using Moonlight.Game.Entities;
using Moonlight.Game.Maps;
using NosCore.Packets.ClientPackets.Drops;

namespace Moonlight.Handlers.Maps
{
    internal class GetPacketHandler : PacketHandler<GetPacket>
    {
        protected override void Handle(Client client, GetPacket packet)
        {
            Map map = client.Character?.Map;

            LivingEntity entity = map?.GetEntity<LivingEntity>(packet.PickerType, packet.PickerId);
            GroundItem groundItem = map?.GetEntity<GroundItem>(packet.VisualId);

            if (entity == null || groundItem == null)
            {
                return;
            }

            map.RemoveEntity(groundItem);
        }
    }
}