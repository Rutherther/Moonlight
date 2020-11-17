using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Core.Enums;
using Moonlight.Core.Logging;
using Moonlight.Event;
using Moonlight.Game.Entities;
using Moonlight.Game.Factory;
using Moonlight.Game.Maps;
using Moonlight.Packet.Map;

namespace Moonlight.Handlers.Maps
{
    public class ThrowPacketHandler : PacketHandler<ThrowPacket>
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IEventManager _eventManager;
        private readonly ILogger _logger;

        public ThrowPacketHandler(ILogger logger, IEntityFactory entityFactory, IEventManager eventManager)
        {
            _logger = logger;
            _entityFactory = entityFactory;
            _eventManager = eventManager;
        }
        
        protected override void Handle(Client client, ThrowPacket packet)
        {
            Map map = client.Character?.Map;

            if (map == null)
            {
                _logger.Warn("Handling InPacket but character map is null");
                return;
            }

            if (map.Contains(EntityType.GROUND_ITEM, packet.TransportId))
            {
                _logger.Warn($"Entity {EntityType.GROUND_ITEM} {packet.TransportId} already on map");
                return;
            }
            
            GroundItem entity = _entityFactory.CreateGroundItem(packet.TransportId, packet.VNum, packet.Amount);
            entity.Position = new Position(packet.PositionX, packet.PositionY);
            
            map.AddEntity(entity);
            _logger.Info($"Item {entity.Id} was thrown on map");
            
            // TODO: thrown event
        }
    }
}