using NosCore.Packets;
using NosCore.Packets.Attributes;

namespace Moonlight.Packet.Raid
{
    [PacketHeader("raidbf")]
    public class RaidBfPacket : PacketBase
    {
        [PacketIndex(1)]
        public int Type { get; set; }
    }
}