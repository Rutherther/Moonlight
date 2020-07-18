using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Core.Logging;
using Moonlight.Game.Entities;
using Moonlight.Game.Factory;
using Moonlight.Game.Inventories;
using Moonlight.Game.Maps;
using NosCore.Packets.ServerPackets.Miniland;

namespace Moonlight.Handlers.Maps.Minilands
{
    internal class MlObjLstPacketHandler : PacketHandler<MlobjlstPacket>
    {
        private readonly ILogger _logger;
        private readonly IMinilandObjectFactory _minilandObjectFactory;

        public MlObjLstPacketHandler(ILogger logger, IMinilandObjectFactory minilandObjectFactory)
        {
            _logger = logger;
            _minilandObjectFactory = minilandObjectFactory;
        }

        protected override void Handle(Client client, MlobjlstPacket packet)
        {
            Character character = client.Character;
            
            var miniland = character?.Map as Miniland;
            if (miniland == null)
            {
                _logger.Info("Not in miniland");
                return;
            }

            miniland.Objects.Clear();

            if (packet.MlobjlstSubPacket != null)
            {
                foreach (MlobjlstSubPacket subPacket in packet.MlobjlstSubPacket)
                {
                    if (subPacket == null)
                    {
                        continue;
                    }

                    int slot = subPacket.Slot;
                    bool used = subPacket.InUse;
                    
                    if (!used)
                    {
                        _logger.Info($"Miniland object {slot} is not used, skipping.");
                        continue;
                    }

                    ItemInstance itemInstance = character.Inventory.Miniland.GetValueOrDefault(slot);
                    if (itemInstance == null)
                    {
                        _logger.Info($"No miniland object found in inventory at slot {slot}");
                        continue;
                    }
                    
                    var position = new Position(subPacket.MlObjSubPacket?.MapX ?? 0, subPacket.MlObjSubPacket?.MapY ?? 0);
                    
                    MinilandObject minilandObject = _minilandObjectFactory.CreateMinilandObject(itemInstance.Item, slot, position);
                    if (minilandObject == null)
                    {
                        _logger.Debug("Not a miniland object");
                        continue;
                    }
                    
                    miniland.Objects.Add(minilandObject);
                }
            }
        }
    }
}