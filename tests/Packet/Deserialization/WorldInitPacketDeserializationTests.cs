using Moonlight.Core.Enums;
using Moonlight.Packet.Core.Serialization;
using Moonlight.Packet.Map.Miniland;
using Moonlight.Packet.WorldInit;
using Moonlight.Tests.Extensions;
using Moonlight.Tests.Utility;
using NFluent;
using Xunit;

namespace Moonlight.Tests.Packet.Deserialization
{
    public class WorldInitPacketDeserializationTests
    {
        public WorldInitPacketDeserializationTests() => _deserializer = TestHelper.CreateDeserializer();

        private readonly IDeserializer _deserializer;

        [Fact]
        public void CList_Packet()
        {
           CListPacket packet = _deserializer.Deserialize<CListPacket>("clist 1 LittleFairy 0 1 0 9 0 0 1 0 -1.12.1.8.-1.-1.-1.-1.-1.-1 1  1 1 -1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1.-1. 0 0");

            Check.That(packet.Slot).Is<byte>(1);
            Check.That(packet.Name).Is("LittleFairy");
            Check.That(packet.Class).Is(ClassType.ADVENTURER);
        }
    }
}