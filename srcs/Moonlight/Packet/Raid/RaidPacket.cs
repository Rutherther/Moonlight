using Moonlight.Core.Enums;
using NosCore.Packets;
using NosCore.Packets.Attributes;

namespace Moonlight.Packet.Raid
{
    [PacketHeader("raid")]
    public class RaidPacket : PacketBase
    {
        [PacketIndex(0)]
        public RaidPacketType Type { get; set; }
        
        [PacketIndex(1)]
        public long LeaderId { get; set; }
    }
}