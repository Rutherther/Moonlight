using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Raid
{
    [PacketHeader("raidbf")]
    internal class RaidBfPacket : Packet
    {
        [PacketIndex(1)]
        public int Type { get; set; }
    }
}
