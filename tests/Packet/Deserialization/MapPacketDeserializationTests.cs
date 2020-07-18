﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moonlight.Core.Enums;
using Moonlight.Packet.Map;
using Moonlight.Tests.Extensions;
using Moonlight.Tests.Utility;
using NFluent;
using NosCore.Packets.ClientPackets.Drops;
using NosCore.Packets.Enumerations;
using NosCore.Packets.Interfaces;
using NosCore.Packets.ServerPackets.Entities;
using NosCore.Packets.ServerPackets.Portals;
using NosCore.Packets.ServerPackets.Visibility;
using NosCore.Shared.Enumerations;
using Xunit;

namespace Moonlight.Tests.Packet.Deserialization
{
    public class MapPacketDeserializationTests
    {
        public MapPacketDeserializationTests() => _deserializer = TestHelper.CreateDeserializer();

        private readonly IDeserializer _deserializer;

        [Fact]
        public void Drop_Packet()
        {
            DropPacket packet = _deserializer.Deserialize<DropPacket>("drop 150 999 15 27 4 0 123456");

            Check.That(packet.VNum).Is<short>(150);
            Check.That(packet.VisualId).Is(999);
            Check.That(packet.PositionX).Is<short>(15);
            Check.That(packet.PositionY).Is<short>(27);
            Check.That(packet.Amount).Is<short>(4);
            Check.That(packet.OwnerId).Is(123456);
        }

        [Fact]
        public void Get_Packet()
        {
            GetPacket packet = _deserializer.Deserialize<GetPacket>("get 1 123456 999 0");

            Check.That(packet.PickerType).Is(VisualType.Player);
            Check.That(packet.PickerId).Is(123456);
            Check.That(packet.VisualId).Is(999);
        }

        [Fact]
        public void Gp_Packet()
        {
            GpPacket packet = _deserializer.Deserialize<GpPacket>("gp 2 123 3 -1 0 0");

            Check.That(packet.SourceX).Is<short>(2);
            Check.That(packet.SourceY).Is<short>(123);
            Check.That(packet.MapId).Is<short>(3);
            Check.That(packet.PortalType).Is(PortalType.MapPortal);
            Check.That(packet.PortalId).Is(0);
        }

        [Fact]
        public void In_Packet_As_Drop()
        {
            InPacket packet = _deserializer.Deserialize<InPacket>("in 9 503 4808544 82 4 1 0 0 -1");

            Check.That(packet.VisualType).Is(VisualType.Object);
            Check.That(packet.VNum).Is("503");
            Check.That(packet.VisualId).Is(4808544);
            Check.That(packet.PositionX).Is<short>(82);
            Check.That(packet.PositionY).Is<short>(4);
            Check.That(packet.InItemSubPacket.Amount).Is(1);
        }

        [Fact]
        public void In_Packet_As_Npc()
        {
            InPacket packet = _deserializer.Deserialize<InPacket>("in 2 334 2206 96 57 2 87 100 0 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0");

            Check.That(packet.VisualType).Is(VisualType.Npc);
            Check.That(packet.VNum).Is("334");
            Check.That(packet.VisualId).Is(2206);
            Check.That(packet.PositionX).Is<short>(96);
            Check.That(packet.PositionY).Is<short>(57);
            Check.That(packet.Direction).Is<byte?>(2);
            Check.That(packet.InNonPlayerSubPacket.InAliveSubPacket.Hp).Is(87);
            Check.That(packet.InNonPlayerSubPacket.InAliveSubPacket.Mp).Is(100);
        }

        [Fact]
        public void In_Packet_As_Pet()
        {
            InPacket packet = _deserializer.Deserialize<InPacket>("in 2 815 749159 75 64 2 100 100 0 0 3 1290807 1 0 -1 Ratufu^centurion 2 -1 0 0 0 0 0 0 0 0");

            Check.That(packet.VisualType).Is(VisualType.Npc);
            Check.That(packet.VNum).Is("815");
            Check.That(packet.VisualId).Is(749159);
            Check.That(packet.PositionX).Is<short>(75);
            Check.That(packet.PositionY).Is<short>(64);
            Check.That(packet.Direction).Is<byte?>(2);
            Check.That(packet.InNonPlayerSubPacket.InAliveSubPacket.Hp).Is(100);
            Check.That(packet.InNonPlayerSubPacket.InAliveSubPacket.Mp).Is(100);
            Check.That(packet.InNonPlayerSubPacket.Owner).Is(1290807);
            Check.That(packet.InNonPlayerSubPacket.Name).Is("Ratufu centurion");
        }

        [Fact]
        public void In_Packet_As_Player()
        {
            InPacket packet = _deserializer.Deserialize<InPacket>(
                "in 1 ~Orage~ - 652158 100 58 2 0 0 0 53 1 204.4949.4964.4955.8026.4130.8205.4179.-1.4443 100 100 0 28 4 2 0 41 0 20 87 87 2582 100ßlaze(Membre) 24 0 15 0 12 92 8 0|0|0 0 35 10 28 93733");

            Check.That(packet.VisualType).Is(VisualType.Player);
            Check.That(packet.Name).Is("~Orage~");
            Check.That(packet.VisualId).Is(652158);
            Check.That(packet.PositionX).Is<short>(100);
            Check.That(packet.PositionY).Is<short>(58);
            Check.That(packet.Direction).Is<byte?>(2);
            Check.That(packet.InCharacterSubPacket.Class).Is(CharacterClassType.Swordsman);
            Check.That(packet.InCharacterSubPacket.Gender).Is(GenderType.Male);
        }

        [Fact]
        public void Out_Packet()
        {
            OutPacket packet = _deserializer.Deserialize<OutPacket>("out 1 128476");

            Check.That(packet.VisualType).Is(VisualType.Player);
            Check.That(packet.VisualId).Is(128476);
        }
    }
}