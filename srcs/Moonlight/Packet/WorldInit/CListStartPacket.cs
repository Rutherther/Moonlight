using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.WorldInit
{
    [PacketHeader("clist_start")]
    public class CListStartPacket : Packet
    {
        [PacketIndex(0)]
        public byte Type { get; set; }
    }
}