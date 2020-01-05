﻿using NtCore.Clients;
using NtCore.Events;
using NtCore.Events.Map;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class MlInfoBrPacketHandler : PacketHandler<MlInfoBrPacket>
    {
        private readonly IEventManager _eventManager;

        public MlInfoBrPacketHandler(IEventManager eventManager) => _eventManager = eventManager;

        public override void Handle(IClient client, MlInfoBrPacket packet)
        {
            if (!(client.Character.Map is Miniland miniland))
            {
                return;
            }

            miniland.Owner = packet.Owner;
            miniland.Visitor = packet.Visitor;
            miniland.TotalVisitor = packet.TotalVisitor;
            miniland.Message = packet.Message;

            _eventManager.CallEvent(new MinilandJoinEvent(client, miniland));
        }
    }
}