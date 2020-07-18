using System.Collections.Generic;
using NosCore.Packets;
using NosCore.Packets.Attributes;

namespace Moonlight.Packet.Dialogs
{
    [PacketHeader("qnamli2")]
    public class Qnamli2Packet : PacketBase
    {
        [PacketIndex(1)]
        public string Command { get; set; }

        [PacketIndex(2)]
        public long Type { get; set; }

        [PacketIndex(3)]
        public int ParametersCount { get; set; }

        public List<string> Parameters { get; set; }
    }
}
