using NosCore.Packets;
using NosCore.Packets.Attributes;
using NosCore.Packets.Enumerations;

namespace Moonlight.Packet.Map
{
    [PacketHeader("die")]
    public class DiePacket : PacketBase
    {
        [PacketIndex(0)]
        public VisualType VisualType { get; set; }
        
        [PacketIndex(1)]
        public long EntityId { get; set; }
    }
}