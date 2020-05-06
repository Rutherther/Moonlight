using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moonlight.Clients;
using Moonlight.Event;
using Moonlight.Event.Raids;
using Moonlight.Game.Raids;
using Moonlight.Packet.Raid;

namespace Moonlight.Handlers.Raids
{
    internal class RaidListPacketHandler : PacketHandler<RaidListPacket>
    {
        private readonly IEventManager _eventManager;

        public RaidListPacketHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        protected override void Handle(Client client, RaidListPacket packet)
        {
            Raid raid = client.Character.Raid;

            if (raid == null || raid.Ended)
            {
                raid = client.Character.Raid = new Raid
                {
                    Status = RaidStatus.Preparation,
                    RaidId = packet.RaidId,
                    MinimumLevel = packet.MinimumLevel,
                    MaximumLevel = packet.MaximumLevel
                };

                _eventManager.Emit(new RaidInitializedEvent(client)
                {
                    Raid = raid
                });
            }

            raid.Players.AddRange(packet.Data.Except(raid.Players));
            raid.Players.RemoveAll(x => !packet.Data.Contains(x));
        }
    }
}
