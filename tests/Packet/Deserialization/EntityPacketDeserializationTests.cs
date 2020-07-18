using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moonlight.Core.Enums;
using Moonlight.Tests.Extensions;
using Moonlight.Tests.Utility;
using NFluent;
using NosCore.Packets.Enumerations;
using NosCore.Packets.Interfaces;
using NosCore.Packets.ServerPackets.Entities;
using NosCore.Packets.ServerPackets.Player;
using Xunit;

namespace Moonlight.Tests.Packet.Deserialization
{
    public class EntityPacketDeserializationTests
    {
        public EntityPacketDeserializationTests() => _deserializer = TestHelper.CreateDeserializer();

        private readonly IDeserializer _deserializer;

        [Fact]
        public void Cond_Packet()
        {
            CondPacket packet = _deserializer.Deserialize<CondPacket>("cond 1 1283965 0 0 12");

            Check.That(packet.VisualType).Is(VisualType.Player);
            Check.That(packet.VisualId).Is(1283965);
            Check.That(packet.NoAttack).IsFalse();
            Check.That(packet.NoMove).IsFalse();
            Check.That(packet.Speed).Is<byte>(12);
        }

        [Fact]
        public void Mv_Packet()
        {
            MovePacket packet = _deserializer.Deserialize<MovePacket>("mv 3 2107 19 7 5");

            Check.That(packet.VisualType).Is(VisualType.Monster);
            Check.That(packet.VisualEntityId).Is(2107);
            Check.That(packet.MapX).Is<short>(19);
            Check.That(packet.MapY).Is<short>(7);
            Check.That(packet.Speed).Is<byte>(5);
        }

        [Fact]
        public void Rest_Packet()
        {
            RestPacket packet = _deserializer.Deserialize<RestPacket>("rest 2 782044 0");

            Check.That(packet.VisualType).Is(VisualType.Npc);
            Check.That(packet.VisualId).Is(782044);
            Check.That(packet.IsSitting).IsFalse();
        }
    }
}