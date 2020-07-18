using Moonlight.Tests.Extensions;
using Moonlight.Tests.Utility;
using NFluent;
using NosCore.Packets.ClientPackets.Miniland;
using NosCore.Packets.Interfaces;
using NosCore.Packets.ServerPackets.Miniland;
using Xunit;

namespace Moonlight.Tests.Packet.Deserialization
{
    public class MinigamePacketDeserialization
    {
        public MinigamePacketDeserialization() => _deserializer = TestHelper.CreateDeserializer();

        private readonly IDeserializer _deserializer;

        [Fact]
        public void Mg_Packet()
        {
            MinigamePacket packet = _deserializer.Deserialize<MinigamePacket>("mg 1 7");

            Check.That(packet.Type).Is<byte>(1);
            Check.That(packet.Id).Is<byte>(7);
        }

        [Fact]
        public void Mlo_Info_Packet()
        {
            MloInfoPacket packet = _deserializer.Deserialize<MloInfoPacket>("mlo_info 1 3120 7 2000 0 0 0 999 1000 3999 4000 7999 8000 11999 12000 19999 20000 1000000");

            Check.That(packet.MinilandPoints).Is(2000);
            Check.That(packet.ObjectVNum).Is<short>(3120);
            Check.That(packet.Slot).Is<byte>(7);
        }

        [Fact]
        public void Mlo_Lv_Packet()
        {
            MloLvPacket packet = _deserializer.Deserialize<MloLvPacket>("mlo_lv 4");

            Check.That(packet.Level).Is<short>(4);
        }

        [Fact]
        public void Mlo_Rw_Packet()
        {
            MloRwPacket packet = _deserializer.Deserialize<MloRwPacket>("mlo_rw 2070 4");

            Check.That(packet.VNum).Is<short>(2070);
            Check.That(packet.Amount).Is<short>(4);
        }

        [Fact]
        public void Mlo_St_Packet()
        {
            MloStPacket packet = _deserializer.Deserialize<MloStPacket>("mlo_st 3");

            Check.That(packet.Header).Is("mlo_st");
        }

        [Fact]
        public void MlPt_Packet()
        {
            MinilandPointPacket packet = _deserializer.Deserialize<MinilandPointPacket>("mlpt 2000 2000");
            Check.That(packet.MinilandPoint).Is<long>(2000);
        }
    }
}