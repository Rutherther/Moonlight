using NosCore.Packets;
using NosCore.Packets.Attributes;

namespace Moonlight.Packet.Battle
{
    [PacketHeader("sr")]
    public class SrPacket : PacketBase
    {
        [PacketIndex(0)]
        public int CastId { get; set; }
    }
}