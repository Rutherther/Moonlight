using System.Linq;
using Moonlight.Clients;
using Moonlight.Event;
using Moonlight.Game.Maps;
using NosCore.Packets.ServerPackets.Miniland;
using Range = Moonlight.Core.Range;

namespace Moonlight.Handlers.Maps.Minilands.Minigames
{
    internal class MloInfoPacketHandler : PacketHandler<MloInfoPacket>
    {
        private readonly IEventManager _eventManager;

        public MloInfoPacketHandler(IEventManager eventManager) => _eventManager = eventManager;

        protected override void Handle(Client client, MloInfoPacket packet)
        {
            var miniland = client?.Character?.Map as Miniland;

            var minigame = miniland?.Objects.FirstOrDefault(x => x.Item.Vnum == packet.ObjectVNum && x.Slot == packet.Slot) as Minigame;
            if (minigame == null)
            {
                return;
            }

            minigame.Scores.Clear();

            if (packet.MinigamePoints != null)
            {
                foreach (MloInfoPacketSubPacket subPacket in packet.MinigamePoints)
                {
                    if (subPacket == null)
                    {
                        continue;
                    }

                    long minimum = subPacket.MinimumPoints, maximum = subPacket.MaximumPoints;

                    if (minimum > 100000)
                    {
                        maximum = minimum + 5000;
                    }

                    minigame.Scores.Add(new Range(minimum, maximum));
                }
            }
        }
    }
}