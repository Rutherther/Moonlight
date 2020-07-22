using System.Collections.Generic;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Raid
{
    [PacketHeader("rdlst", "rdlstf")]
    internal class RaidListPacket : Packet
    {
        public int MinimumLevel { get; set; }
        public int MaximumLevel { get; set; }
        public int RaidId { get; set; }
        public List<RaidPlayerData> Data { get; set; }
    }
}
