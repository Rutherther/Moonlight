using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moonlight.Clients;
using Moonlight.Game.Raids;
using Moonlight.Packet.Raid;

namespace Moonlight.Handlers.Raids
{
    public class RaidListPacketHandler : PacketHandler<RaidListPacket>
    {
        protected override void Handle(Client client, RaidListPacket packet)
        {
            Raid raid = client.Character.Raid;

            if (raid == null || raid.Ended)
            {
                client.Character.Raid = new Raid
                {
                    Status = RaidStatus.Preparation,
                    RaidId = packet.RaidId,
                    MinimumLevel = packet.MinimumLevel,
                    MaximumLevel = packet.MaximumLevel
                };
            }

            // TODO: handle players
        }
    }
}
