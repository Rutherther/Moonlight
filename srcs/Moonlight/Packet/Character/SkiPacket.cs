using System.Collections.Generic;
using NosCore.Packets;
using NosCore.Packets.Attributes;

namespace Moonlight.Packet.Character
{
    [PacketHeader("ski")]
    public class SkiPacket : PacketBase
    {
        [PacketIndex(0)]
        public List<int> Skills { get; set; }
    }
}