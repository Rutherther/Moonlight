using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Event;
using Moonlight.Event.Entities;
using Moonlight.Game.Entities;
using NosCore.Packets.ServerPackets.Entities;

namespace Moonlight.Handlers.Entities
{
    internal class MvPacketHandler : PacketHandler<MovePacket>
    {
        private readonly IEventManager _eventManager;

        public MvPacketHandler(IEventManager eventManager) => _eventManager = eventManager;

        protected override void Handle(Client client, MovePacket packet)
        {
            LivingEntity entity = client.Character?.Map?.GetEntity<LivingEntity>(packet.VisualType, packet.VisualEntityId);

            if (entity == null)
            {
                return;
            }

            Position from = entity.Position;

            entity.Position = new Position(packet.MapX, packet.MapY);
            entity.Speed = packet.Speed;

            _eventManager.Emit(new EntityMoveEvent(client)
            {
                Entity = entity,
                From = from,
                To = entity.Position
            });
        }
    }
}