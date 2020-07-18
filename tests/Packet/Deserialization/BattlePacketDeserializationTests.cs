using Moonlight.Packet.Battle;
using Moonlight.Tests.Extensions;
using Moonlight.Tests.Utility;
using NFluent;
using NosCore.Packets.Enumerations;
using NosCore.Packets.Interfaces;
using NosCore.Packets.ServerPackets.Battle;
using Xunit;

namespace Moonlight.Tests.Packet.Deserialization
{
    public class BattlePacketDeserializationTests
    {
        public BattlePacketDeserializationTests() => _deserializer = TestHelper.CreateDeserializer();

        private readonly IDeserializer _deserializer;

        [Fact]
        public void Sr_Packet()
        {
            SrPacket packet = _deserializer.Deserialize<SrPacket>("sr 0");

            Check.That(packet).IsNotNull();
            Check.That(packet.CastId).IsEqualTo(0);
        }

        [Fact]
        public void Su_Packet()
        {
            SuPacket packet = _deserializer.Deserialize<SuPacket>("su 1 12345 3 2080 240 8 11 257 0 0 0 0 723 0 0");

            Check.That(packet.VisualType).Is(VisualType.Player);
            Check.That(packet.VisualId).Is(12345);
            Check.That(packet.TargetVisualType).Is(VisualType.Monster);
            Check.That(packet.TargetId).Is(2080);
            Check.That(packet.SkillVnum).Is(240);
            Check.That(packet.TargetIsAlive).IsFalse();
            Check.That(packet.HpPercentage).Is<byte>(0);
            Check.That(packet.Damage).Is<uint>(723);
        }
    }
}