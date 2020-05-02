using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moonlight.Clients;
using Moonlight.Game.Entities;
using Moonlight.Game.Raids;
using Moonlight.Packet.Raid;

namespace Moonlight.Handlers.Raids
{
    internal class RaidBossPacketHandler : PacketHandler<RaidBossPacket>

    {
        protected override void Handle(Client client, RaidBossPacket packet)
        {
            Raid raid = client.Character.Raid;

            if (raid.Status == RaidStatus.Unknown)
            {
                raid.Status = RaidStatus.InProgress;
            }

            if (raid.Boss == null)
            {
                raid.Boss = client.Character.Map.GetEntity<Monster>(packet.MonsterId);
                raid.Boss.IsRaidBoss = true;

                raid.Bosses.Add(raid.Boss);
            }
            else if (raid.Boss.Id != packet.MonsterId && !raid.Bosses.Exists(x => x.Id == packet.MonsterId))
            {
                Monster anotherRaidBoss = client.Character.Map.GetEntity<Monster>(packet.MonsterId);
                anotherRaidBoss.IsRaidBoss = true;

                raid.Bosses.Add(anotherRaidBoss);
            }
        }
    }
}
