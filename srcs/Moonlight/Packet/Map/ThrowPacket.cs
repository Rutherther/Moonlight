using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Packet.Map
{
    [PacketHeader("throw")]
    public class ThrowPacket : Packet
    {
        //throw {droppedItem.ItemVNum} {droppedItem.TransportId} {originX} {originY} {droppedItem.PositionX} {droppedItem.PositionY} {(droppedItem.GoldAmount > 1 ? droppedItem.GoldAmount : droppedItem.Amount)}
        
        [PacketIndex(0)]
        public int VNum { get; set; }
        
        [PacketIndex(1)]
        public long TransportId { get; set; }
        
        [PacketIndex(2)]
        public short OriginX { get; set; }
        
        [PacketIndex(3)]
        public short OriginY { get; set; }
        
        [PacketIndex(4)]
        public short PositionX { get; set; }
        
        [PacketIndex(5)]
        public short PositionY { get; set; }
        
        [PacketIndex(6)]
        public int Amount { get; set; }
    }
}