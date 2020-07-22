using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Entity
{
    [PacketHeader("revive")]
    public class RevivePacket : Packet
    {
        [PacketIndex(0)]
        public short Unknown1 { get; set; }
        
        [PacketIndex(1)]
        public long EntityId { get; set; }
        
        [PacketIndex(2)]
        public short Unknown2 { get; set; }
    }
}