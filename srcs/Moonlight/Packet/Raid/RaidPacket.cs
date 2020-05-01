using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Raid
{
    [PacketHeader("raid")]
    internal class RaidPacket : Packet
    {
        [PacketIndex(0)]
        public int Type { get; set; }

        [PacketIndex(1)]
        public int Data { get; set; }
    }
}
