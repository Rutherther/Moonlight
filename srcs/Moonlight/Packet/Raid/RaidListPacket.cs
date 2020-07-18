using System.Collections.Generic;
using NosCore.Packets;
using NosCore.Packets.Attributes;

namespace Moonlight.Packet.Raid
{
    [PacketHeader("rdlst")]
    public class RaidListPacket : PacketBase
    {
        [PacketIndex(0)]
        public int MinimumLevel { get; set; }
        
        [PacketIndex(1)]
        public int MaximumLevel { get; set; }
        
        [PacketIndex(2)]
        public int RaidId { get; set; }
        
        [PacketIndex(3, SpecialSeparator = ".")]
        public List<RaidListSubPacket> RaidPlayers { get; set; }
    }
}