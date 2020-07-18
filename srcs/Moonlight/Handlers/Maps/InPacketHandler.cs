using System;
using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Core.Logging;
using Moonlight.Event;
using Moonlight.Event.Maps;
using Moonlight.Game.Entities;
using Moonlight.Game.Factory;
using Moonlight.Game.Maps;
using NosCore.Packets.Enumerations;
using NosCore.Packets.ServerPackets.Visibility;
using NosCore.Shared.Enumerations;

namespace Moonlight.Handlers.Maps
{
    internal class InPacketHandler : PacketHandler<InPacket>
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IEventManager _eventManager;
        private readonly ILogger _logger;

        public InPacketHandler(ILogger logger, IEntityFactory entityFactory, IEventManager eventManager)
        {
            _logger = logger;
            _entityFactory = entityFactory;
            _eventManager = eventManager;
        }

        protected override void Handle(Client client, InPacket packet)
        {
            Map map = client.Character?.Map;

            if (map == null)
            {
                _logger.Warn("Handling InPacket but character map is null");
                return;
            }

            if (map.Contains(packet.VisualType, packet.VisualId))
            {
                _logger.Warn($"Entity {packet.VisualType} {packet.VisualId} already on map");
                return;
            }

            Entity entity;
            int Vnum = int.Parse(packet.VNum ?? "");
            switch (packet.VisualType)
            {
                case VisualType.Monster:
                    entity = _entityFactory.CreateMonster(packet.VisualId, Vnum);
                    break;
                case VisualType.Npc:
                    entity = _entityFactory.CreateNpc(packet.VisualId, Vnum, packet.InNonPlayerSubPacket?.Name);
                    break;
                case VisualType.Player:
                    entity = _entityFactory.CreatePlayer(packet.VisualId, packet.Name);
                    break;
                case VisualType.Object:
                    entity = _entityFactory.CreateGroundItem(packet.VisualId, Vnum, packet.InItemSubPacket?.Amount ?? 0);
                    break;
                default:
                    throw new InvalidOperationException("Undefined entity type");
            }

            entity.Position = new Position(packet.PositionX, packet.PositionY);

            if (entity is Player player)
            {
                player.Level = packet.InCharacterSubPacket?.Level ?? 0;
                player.Class = packet.InCharacterSubPacket?.Class ?? CharacterClassType.Adventurer;
                player.Gender = packet.InCharacterSubPacket?.Gender ?? GenderType.Male;
                player.Direction = packet.Direction ?? 0;
                player.HpPercentage = packet.InCharacterSubPacket?.InAliveSubPacket?.Hp ?? 0;
                player.MpPercentage = packet.InCharacterSubPacket?.InAliveSubPacket?.Mp ?? 0;
            }

            if (entity is Monster monster)
            {
                monster.Direction = packet.Direction ?? 0;
                monster.HpPercentage = packet.InNonPlayerSubPacket?.InAliveSubPacket?.Hp ?? 0;
                monster.MpPercentage = packet.InNonPlayerSubPacket?.InAliveSubPacket?.Mp ?? 0;
                monster.Faction = (FactionType)(packet.InNonPlayerSubPacket?.Faction ?? 0);
            }

            if (entity is Npc npc)
            {
                npc.Direction = packet.Direction ?? 0;
                npc.HpPercentage = packet.InNonPlayerSubPacket?.InAliveSubPacket?.Hp ?? 0;
                npc.MpPercentage = packet.InNonPlayerSubPacket?.InAliveSubPacket?.Mp ?? 0;
                npc.Faction = (FactionType)(packet.InNonPlayerSubPacket?.Faction ?? 0);
            }

            if (entity is GroundItem drop)
            {
                drop.Owner = map.GetEntity<Player>(packet.InItemSubPacket?.Owner ?? 0);
            }

            map.AddEntity(entity);
            _logger.Info($"Entity {entity.VisualType} {entity.Id} joined map");

            _eventManager.Emit(new EntityJoinEvent(client)
            {
                Map = map,
                Entity = entity
            });
        }
    }
}
