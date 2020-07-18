using Moonlight.Clients;
using Moonlight.Game.Entities;
using NosCore.Packets.ServerPackets.Specialists;

namespace Moonlight.Handlers.Characters
{
    internal class SpPacketHandler : PacketHandler<SpPacket>
    {
        protected override void Handle(Client client, SpPacket packet)
        {
            Character character = client.Character;

            if (character != null)
            {
                character.SpPoints = packet.SpPoint;
                character.AdditionalSpPoints = packet.AdditionalPoint;
            }
        }
    }
}