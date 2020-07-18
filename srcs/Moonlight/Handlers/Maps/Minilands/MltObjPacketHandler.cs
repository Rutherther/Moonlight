using System;
using Moonlight.Clients;
using Moonlight.Core;
using Moonlight.Game.Entities;
using Moonlight.Game.Factory;
using Moonlight.Game.Maps;
using Moonlight.Packet.Miniland;
using NosCore.Packets;
using NosCore.Packets.Enumerations;

namespace Moonlight.Handlers.Maps.Minilands
{
    internal class MltObjPacketHandler : PacketHandler<MltobjPacket>
    {
        private readonly IMinilandObjectFactory _minilandObjectFactory;

        public MltObjPacketHandler(IMinilandObjectFactory minilandObjectFactory) => _minilandObjectFactory = minilandObjectFactory;

        protected override void Handle(Client client, MltobjPacket packet)
        {
            Character character = client.Character;

            var miniland = character?.Map as Miniland;
            if (miniland == null)
            {
                return;
            }

            miniland.Objects.Clear();

            if (packet.MinilandObjectSubPackets != null)
            {
                foreach (MltobjSubPacket subPacket in packet.MinilandObjectSubPackets)
                {
                    if (subPacket == null)
                    {
                        continue;
                    }
                    
                    var position = new Position(subPacket.MapX, subPacket.MapY);
                    MinilandObject minilandObject = _minilandObjectFactory.CreateMinilandObject(subPacket.Vnum, subPacket.Slot, position);
                    if (minilandObject == null)
                    {
                        continue;
                    }

                    miniland.Objects.Add(minilandObject);
                }
            }
        }
    }
}