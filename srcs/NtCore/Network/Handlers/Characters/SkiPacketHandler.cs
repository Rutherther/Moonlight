﻿using NtCore.Clients;
using NtCore.Factory;
using NtCore.Game.Battle;
using NtCore.Game.Entities;
using NtCore.Network.Packets.Characters;

namespace NtCore.Network.Handlers.Characters
{
    public class SkiPacketHandler : PacketHandler<SkiPacket>
    {
        private readonly ISkillFactory _skillFactory;

        public SkiPacketHandler(ISkillFactory skillFactory)
        {
            _skillFactory = skillFactory;
        }
        
        public override void Handle(IClient client, SkiPacket packet)
        {
            ICharacter character = client.Character;
            
            character.Skills.Clear();
            
            foreach (int skillVnum in packet.Skills)
            {
                ISkill skill = _skillFactory.CreateSkill(skillVnum);
                character.Skills.Add(skill);
            }
        }
    }
}