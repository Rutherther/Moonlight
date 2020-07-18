using NosCore.Packets;
using NosCore.Packets.Attributes;

namespace Moonlight.Packet.Raid
{
    [PacketHeader("rboss")]
    public class RaidBossPacket : PacketBase
    {
        [PacketIndex(1)]
        public int MonsterId { get; set; }

        [PacketIndex(2)]
        public int Hp { get; set; }

        [PacketIndex(3)]
        public int HpMaximum { get; set; }

        [PacketIndex(4)]
        public int Vnum { get; set; }
    }
}