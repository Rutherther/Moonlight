using NosCore.Packets;
using NosCore.Packets.Attributes;
using NosCore.Shared.Enumerations;

namespace Moonlight.Packet.Raid
{
    public class RaidListSubPacket : PacketBase
    {
        [PacketIndex(0)]
        public int Level { get; set; }
        
        [PacketIndex(2)]
        public CharacterClassType Class { get; set; }
        
        [PacketIndex(4)]
        public string Name { get; set; }
        
        [PacketIndex(6)]
        public int Id { get; set; }
        
        [PacketIndex(7)]
        public int HeroLevel { get; set; }
    }
}