using NosCore.Packets;
using NosCore.Packets.Attributes;

namespace Moonlight.Packet.Raid
{
    [PacketHeader("raidmbf")]
    public class RaidMbfPacket : PacketBase
    {
        [PacketIndex(0)]
        public int MonsterLockerInitial { get; set; }

        [PacketIndex(1)]
        public int MonsterLockerCurrent { get; set; }

        [PacketIndex(2)]
        public int ButtonLockerInitial { get; set; }

        [PacketIndex(3)]
        public int ButtonLockerCurrent { get; set; }

        [PacketIndex(4)]
        public int CurrentLifes { get; set; }

        [PacketIndex(5)]
        public int InitialLifes { get; set; }
    }
}