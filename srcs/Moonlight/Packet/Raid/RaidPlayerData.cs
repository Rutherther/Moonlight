using Moonlight.Core.Enums;

namespace Moonlight.Packet.Raid
{
    public class RaidPlayerData
    {
        public int Level { get; set; }

        public int ChampionLevel { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }

        public ClassType Class { get; set; }
    }
}
