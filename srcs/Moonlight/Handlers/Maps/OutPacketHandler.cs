using Moonlight.Clients;
using Moonlight.Core.Logging;
using Moonlight.Event;
using Moonlight.Event.Maps;
using Moonlight.Game.Entities;
using Moonlight.Game.Maps;
using NosCore.Packets.ServerPackets.Entities;

namespace Moonlight.Handlers.Maps
{
    internal class OutPacketHandler : PacketHandler<OutPacket>
    {
        private readonly IEventManager _eventManager;
        private readonly ILogger _logger;

        public OutPacketHandler(ILogger logger, IEventManager eventManager)
        {
            _logger = logger;
            _eventManager = eventManager;
        }

        protected override void Handle(Client client, OutPacket packet)
        {
            Map map = client.Character?.Map;

            if (map == null)
            {
                _logger.Warn("Handling OutPacket but character map is null");
                return;
            }

            Entity entity = map.GetEntity(packet.VisualType, packet.VisualId);
            if (entity == null)
            {
                _logger.Warn($"Can't found entity {packet.VisualType} {packet.VisualId} in map");
                return;
            }

            map.RemoveEntity(packet.VisualType, packet.VisualId);
            _logger.Info($"Entity {entity.VisualType} {entity.Id} leaved map");

            _eventManager.Emit(new EntityLeaveEvent(client)
            {
                Map = map,
                Entity = entity
            });
        }
    }
}