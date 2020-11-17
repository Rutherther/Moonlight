using Moonlight.Clients;
using Moonlight.Event;
using Moonlight.Event.Maps;
using Moonlight.Game.Entities;
using Moonlight.Game.Maps;
using Moonlight.Packet.Map;

namespace Moonlight.Handlers.Maps
{
    internal class GetPacketHandler : PacketHandler<GetPacket>
    {
        private IEventManager _eventManager;
        
        public GetPacketHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }
        
        protected override void Handle(Client client, GetPacket packet)
        {
            Map map = client.Character?.Map;

            LivingEntity entity = map?.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
            GroundItem groundItem = map?.GetEntity<GroundItem>(packet.DropId);

            if (entity == null || groundItem == null)
            {
                return;
            }
            
            _eventManager.Emit(new ItemPickedUpEvent(client)
            {
                Item = groundItem,
                Map = map,
                Entity = entity
            });

            map.RemoveEntity(groundItem);
        }
    }
}