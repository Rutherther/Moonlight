using System;
using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Login
{
    [PacketHeader("NoS0577")]
    public class NoS0577Packet : Packet
    {
        [PacketIndex(0)]
        public string AuthToken { get; set; }

        [PacketIndex(1)]
        public Guid ClientId { get; set; }

        [PacketIndex(2)]
        public string UnknownYet { get; set; }

        [PacketIndex(3, Separator = "")]
        public RegionType RegionType { get; set; }

        [PacketIndex(4)]
        public string ClientVersion { get; set; }

        [PacketIndex(5)]
        public int UnknownConstant { get; set; }

        [PacketIndex(6)]
        public string Md5String { get; set; }
    }
}