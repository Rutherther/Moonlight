using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.World
{
    [PacketHeader("pulse")]
    public class PulsePacket : Packet
    {
        [PacketIndex(0)]
        public long Tick { get; set; }
        
        [PacketIndex(1)]
        public int State { get; set; }
    }
}