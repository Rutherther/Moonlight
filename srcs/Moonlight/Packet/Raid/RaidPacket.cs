using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Raid
{
    [PacketHeader("raid")]
    internal class RaidPacket : Packet
    {
        public RaidPacketType Type { get; set; }

        public long LeaderId { get; set; }
    }
}
