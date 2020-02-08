﻿using System.Collections.Generic;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Character
{
    [PacketHeader("ski")]
    internal class SkiPacket : Packet
    {
        public List<int> Skills { get; set; }
    }
}