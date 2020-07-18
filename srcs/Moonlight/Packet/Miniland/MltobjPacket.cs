#nullable enable
using System.Collections.Generic;
using NosCore.Packets;
using NosCore.Packets.Attributes;

namespace Moonlight.Packet.Miniland
{
    [PacketHeader("mltobj")]
    public class MltobjPacket : PacketBase
    {
        public List<MltobjSubPacket?>? MinilandObjectSubPackets {get; set;}
    }
}