using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Login
{
    [PacketHeader("failc")]
    public class FailcPacket : Packet
    {
        [PacketIndex(0)]
        public LoginFailType Type { get; set; }
    }
}