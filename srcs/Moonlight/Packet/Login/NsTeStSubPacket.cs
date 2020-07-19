using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Login
{
    public class NsTeStSubPacket : Packet
    {
        [PacketIndex(0)]
        public string Host { get; set; }

        [PacketIndex(1, Separator = ":")]
        public int? Port { get; set; }

        [PacketIndex(2, Separator = ":")]
        public int? Color { get; set; }

        [PacketIndex(3, Separator = ":")]
        public int WorldCount { get; set; }

        [PacketIndex(4, Separator = ".")]
        public int WorldId { get; set; }

        [PacketIndex(5, Separator = ".")]
        public string Name { get; set; }
    }
}