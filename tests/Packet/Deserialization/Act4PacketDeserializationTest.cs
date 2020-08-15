using Moonlight.Core.Enums;
using Moonlight.Packet.Act4;
using Moonlight.Packet.Character.Inventory;
using Moonlight.Packet.Core.Serialization;
using Moonlight.Tests.Extensions;
using Moonlight.Tests.Utility;
using NFluent;
using Xunit;

namespace Moonlight.Tests.Packet.Deserialization
{
    public class Act5PacketDeserializationTest
    {
        private readonly IDeserializer _deserializer;
        
        public Act5PacketDeserializationTest() => _deserializer = TestHelper.CreateDeserializer();

        [Fact]
        public void Fc_CheckCorrectValues()
        {
            FcPacket packet = _deserializer.Deserialize<FcPacket>("fc 1 29257 88 1 2 0 0 0 1 0 0 42 0 0 0 0 0 0 0 0");

            Check.That(packet.Faction).IsEqualTo(FactionType.ANGEL);
            Check.That(packet.MinutesUntilReset).IsEqualTo(29257);
            
            Check.That(packet.AngelState.Percentage).IsEqualTo(88);
            Check.That(packet.AngelState.Mode).IsEqualTo(Act4Mode.None);
            Check.That(packet.AngelState.CurrentTime).IsEqualTo(1);
            Check.That(packet.AngelState.TotalTime).IsEqualTo(2);
            Check.That(packet.AngelState.IsCalvina).IsEqualTo(false);
            Check.That(packet.AngelState.IsBerios).IsEqualTo(false);
            Check.That(packet.AngelState.IsHatus).IsEqualTo(false);
            Check.That(packet.AngelState.IsMorcos).IsEqualTo(false);
            
            Check.That(packet.DemonState.Percentage).IsEqualTo(42);
            Check.That(packet.DemonState.Mode).IsEqualTo(Act4Mode.None);
            Check.That(packet.DemonState.CurrentTime).IsEqualTo(0);
            Check.That(packet.DemonState.TotalTime).IsEqualTo(0);
            Check.That(packet.DemonState.IsCalvina).IsEqualTo(false);
            Check.That(packet.DemonState.IsBerios).IsEqualTo(false);
            Check.That(packet.DemonState.IsHatus).IsEqualTo(false);
            Check.That(packet.DemonState.IsMorcos).IsEqualTo(false);
        }
        

    }
}