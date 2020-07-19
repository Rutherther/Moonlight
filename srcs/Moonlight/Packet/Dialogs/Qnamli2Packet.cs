using System;
using System.Collections.Generic;
using System.Text;
using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Dialogs
{
    [PacketHeader("qnamli2")]
    public class Qnamli2Packet : Packet
    {
        [PacketIndex(1)]
        public string Command { get; set; }

        [PacketIndex(2)]
        public Game18NConstString Type { get; set; }

        [PacketIndex(3)]
        public int ParametersCount { get; set; }

        public string[] Parameters { get; set; }
    }
}
