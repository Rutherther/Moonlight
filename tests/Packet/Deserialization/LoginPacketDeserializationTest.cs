using Moonlight.Core.Enums;
using Moonlight.Packet.Character.Inventory;
using Moonlight.Packet.Core.Serialization;
using Moonlight.Packet.Login;
using Moonlight.Tests.Extensions;
using Moonlight.Tests.Utility;
using NFluent;
using Xunit;

namespace Moonlight.Tests.Packet.Deserialization
{
    public class LoginPacketDeserializationTest
    {
        public LoginPacketDeserializationTest() => _deserializer = TestHelper.CreateDeserializer();

        private readonly IDeserializer _deserializer;
        
        [Fact]
        public void NsTeST_Aeros_Server()
        {
            NsTestPacket packet = _deserializer.Deserialize<NsTestPacket>("NsTeST  6 rutherther 2 56344 79.110.84.41:4014:0:1.5.Aeros 79.110.84.41:4012:0:1.3.Aeros 79.110.84.41:4013:0:1.4.Aeros 79.110.84.41:4010:2:1.1.Aeros 79.110.84.41:4011:0:1.2.Aeros -1:-1:-1:10000.10000.1");

            Check.That(packet.RegionType).Is(RegionType.CZ);
            Check.That(packet.AccountName).Is("rutherther");
            Check.That(packet.Unknown).Is(2);
            Check.That(packet.SessionId).Is(56344);

            Check.That(packet.NsTestSubPackets).CountIs(6);
            Check.That(packet.NsTestSubPackets).HasElementAt(0).WhichMatch(x => x.Host == "79.110.84.41" && x.Port == 4014 && x.Name == "Aeros" && x.WorldId == 5 && x.Color == 0 && x.WorldCount == 1);
            Check.That(packet.NsTestSubPackets).HasElementAt(1).WhichMatch(x => x.Host == "79.110.84.41" && x.Port == 4012 && x.Name == "Aeros" && x.WorldId == 3 && x.Color == 0 && x.WorldCount == 1);
            Check.That(packet.NsTestSubPackets).HasElementAt(2).WhichMatch(x => x.Host == "79.110.84.41" && x.Port == 4013 && x.Name == "Aeros" && x.WorldId == 4 && x.Color == 0 && x.WorldCount == 1);
            Check.That(packet.NsTestSubPackets).HasElementAt(3).WhichMatch(x => x.Host == "79.110.84.41" && x.Port == 4010 && x.Name == "Aeros" && x.WorldId == 1 && x.Color == 2 && x.WorldCount == 1);
            Check.That(packet.NsTestSubPackets).HasElementAt(4).WhichMatch(x => x.Host == "79.110.84.41" && x.Port == 4011 && x.Name == "Aeros" && x.WorldId == 2 && x.Color == 0 && x.WorldCount == 1);
            Check.That(packet.NsTestSubPackets).HasElementAt(5).WhichMatch(x => x.Host == "-1" && x.Port == null && x.Name == "1" && x.WorldId == 10000 && x.Color == null && x.WorldCount == 10000);
        }
    }
}