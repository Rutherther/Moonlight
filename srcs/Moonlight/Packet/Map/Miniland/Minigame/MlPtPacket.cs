﻿using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Map.Miniland.Minigame
{
    [PacketHeader("mlpt")]
    internal class MlPtPacket : Packet
    {
        [PacketIndex(0)]
        public short Points { get; set; }
    }
}