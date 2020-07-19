using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.WorldInit
{
    [PacketHeader("select")]
    public class SelectPacket : Packet
    {
        [PacketIndex(0)]
        public short Slot { get; set; }
    }
}