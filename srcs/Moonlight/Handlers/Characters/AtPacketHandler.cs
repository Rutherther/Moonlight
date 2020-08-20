using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Event;
using Moonlight.Event.Maps;
using Moonlight.Game.Entities;
using Moonlight.Game.Factory;
using Moonlight.Game.Maps;
using Moonlight.Packet.Character;

namespace Moonlight.Handlers.Characters
{
    internal class AtPacketHandler : PacketHandler<AtPacket>
    {
        private readonly IMapFactory _mapFactory;
        private readonly IEventManager _eventManager;

        public AtPacketHandler(IMapFactory mapFactory, IEventManager eventManager)
        {
            _eventManager = eventManager;
            _mapFactory = mapFactory;
        }

        protected override void Handle(Client client, AtPacket packet)
        {
            if (client.Character == null)
            {
                return;
            }

            Character character = client.Character;
            if (packet.CharacterId != character.Id)
            {
                return;
            }

            Map map = _mapFactory.CreateMap(packet.MapId);
            Map source = character.Map;
            bool mapChange = character.Map == null || character.Map.Id != packet.MapId;
            
            map.AddEntity(character);
            character.Position = new Position(packet.PositionX, packet.PositionY);

            if (mapChange)
            {
                _eventManager.Emit(new MapChangeEvent(client)
                {
                    Character = client.Character,
                    Source = source,
                    Destination = map
                });
            }
        }
    }
}