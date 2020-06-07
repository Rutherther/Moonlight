using System;
using System.Collections.Generic;
using System.Text;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Chat
{
    [PacketHeader("sayi")]
    public class SayiPacket : Packet
    {
        [PacketIndex(0)]
        public int Type { get; set; }

        [PacketIndex(1)]
        public long Id { get; set; }


        [PacketIndex(2)]
        public byte Color { get; set; }

        [PacketIndex(3)]
        public long Message { get; set; }

        [PacketIndex(4)]
        public long Param1 { get; set; }

        [PacketIndex(5)]
        public long Param2 { get; set; }

        [PacketIndex(6)]
        public long Param3 { get; set; }

        [PacketIndex(7)]
        public long Param4 { get; set; }

        [PacketIndex(8)]
        public long Param5 { get; set; }
    }
}
