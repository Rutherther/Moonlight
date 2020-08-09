using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.World
{
    [PacketHeader("mz")]
    public class MzPacket : Packet
    {
        [PacketIndex(0)]
        public string Ip { get; set; }
        
        [PacketIndex(1)]
        public short Port { get; set; }
    }
}