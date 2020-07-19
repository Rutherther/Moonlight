using System.Collections.Generic;
using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Login
{
    [PacketHeader("NsTeST")]
    public class NsTestPacket : Packet
    {
        [PacketIndex(0)]
        public RegionType RegionType { get; set; }

        [PacketIndex(1)]
        public string AccountName { get; set; }

        //this seems to be always 2 in case of new auth and null else
        [PacketIndex(2)]
        public int? Unknown { get; set; }

        [PacketIndex(3)]
        public int SessionId { get; set; }
        
        [PacketIndex(4, TillEnd = true)]
        public List<NsTeStSubPacket> NsTestSubPackets { get; set; }
    }
}