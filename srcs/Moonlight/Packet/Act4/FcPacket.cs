using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Act4
{
    [PacketHeader("fc")]
    public class FcPacket : Packet
    {
        [PacketIndex(0)]
        public FactionType Faction { get; set; }
        
        [PacketIndex(1)]
        public long MinutesUntilReset { get; set; }
        
        [PacketIndex(2, TillEnd = true)]
        public FactionFcSubPacket AngelState { get; set; }
        
        [PacketIndex(11, TillEnd = true)]
        public FactionFcSubPacket DemonState { get; set; }
    }
}