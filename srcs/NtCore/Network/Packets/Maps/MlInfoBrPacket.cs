﻿namespace NtCore.Network.Packets.Maps
{
    [PacketInfo("mlinfobr", PacketType.Recv)]
    public class MlInfoBrPacket : Packet
    {
        [PacketIndex(2)] public string Owner { get; set; }

        [PacketIndex(3)] public int Visitor { get; set; }

        [PacketIndex(4)] public int TotalVisitor { get; set; }

        [PacketIndex(6)] public string Message { get; set; }

        public override bool Deserialize(string[] packet)
        {
            base.Deserialize(packet);

            Message = Message.Replace("^", " ");
            return true;
        }
    }
}