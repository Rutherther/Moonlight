using NosCore.Packets.Attributes;

namespace Moonlight.Packet.Miniland
{
    public class MltobjSubPacket
    {
        [PacketIndex(0)]
        public int Vnum { get; set; }
        
        [PacketIndex(1)]
        public int Slot { get; set; }
        
        public short MapX { get; set; }
        
        public short MapY { get; set; }
    }
}