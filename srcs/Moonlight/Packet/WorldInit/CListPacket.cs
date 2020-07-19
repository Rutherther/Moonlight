using System.Collections.Generic;
using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.WorldInit
{
    [PacketHeader("clist")]
    public class CListPacket : Packet
    {
        [PacketIndex(0)]
        public byte Slot { get; set; }

        [PacketIndex(1)]
        public string Name { get; set; }

        [PacketIndex(2)]
        public byte Unknown { get; set; } //TODO to find

        [PacketIndex(3)]
        public GenderType Gender { get; set; }

        [PacketIndex(4)]
        public HairStyleType HairStyle { get; set; }

        [PacketIndex(5)]
        public HairColorType HairColor { get; set; }

        [PacketIndex(6)]
        public byte Unknown1 { get; set; } //TODO to find

        [PacketIndex(7)]
        public ClassType Class { get; set; }

        [PacketIndex(8)]
        public byte Level { get; set; }

        [PacketIndex(9)]
        public byte HeroLevel { get; set; }

        [PacketIndex(10, ListSeparator = ".")]
        public List<short?> Equipments { get; set; }

        [PacketIndex(11)]
        public byte JobLevel { get; set; }

        [PacketIndex(12)] 
        public string ExtraSpace { get; set; } = string.Empty;

        [PacketIndex(13)]
        public byte QuestCompletion { get; set; }

        [PacketIndex(14)]
        public byte QuestPart { get; set; }

        [PacketIndex(15, ListSeparator = ".")]
        public List<short?> Pets { get; set; }

        [PacketIndex(16)]
        public int Design { get; set; }

        [PacketIndex(17)]
        public bool Rename { get; set; }
    }
}