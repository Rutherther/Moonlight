using Moonlight.Clients;
using Moonlight.Game.Entities;
using NosCore.Packets.ServerPackets.Player;

namespace Moonlight.Handlers.Characters
{
    internal class LevPacketHandler : PacketHandler<LevPacket>
    {
        protected override void Handle(Client client, LevPacket packet)
        {
            Character character = client.Character;
            if (character != null)
            {
                character.Level = packet.Level;
                character.JobLevel = packet.JobLevel;
            }
        }
    }
}