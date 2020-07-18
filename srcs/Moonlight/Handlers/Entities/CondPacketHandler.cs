using Moonlight.Clients;
using Moonlight.Game.Entities;
using Moonlight.Game.Maps;
using NosCore.Packets.ServerPackets.Player;

namespace Moonlight.Handlers.Entities
{
    internal class CondPacketHandler : PacketHandler<CondPacket>
    {
        protected override void Handle(Client client, CondPacket packet)
        {
            Map map = client.Character?.Map;

            LivingEntity entity = map?.GetEntity<LivingEntity>(packet.VisualType, packet.VisualId);
            if (entity == null)
            {
                return;
            }

            entity.Speed = packet.Speed;
        }
    }
}