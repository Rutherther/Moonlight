﻿using System;
using NtCore.API.Clients;
using NtCore.API.Events.Map;
using NtCore.API.Extensions;
using NtCore.API.Managers;
using NtCore.API.Plugins;
using NtCore.Game.Entities;
using NtCore.Game.Maps;
using NtCore.Network.Packets.Maps;

namespace NtCore.Network.Handlers.Maps
{
    public class CMapPacketHandler : PacketHandler<CMapPacket>
    {
        private readonly IMapManager _mapManager;
        private readonly IPluginManager _pluginManager;

        public CMapPacketHandler(IMapManager mapManager, IPluginManager pluginManager)
        {
            _pluginManager = pluginManager;
            _mapManager = mapManager;
        }

        public override void Handle(IClient client, CMapPacket packet)
        {
            var character = client.Character.As<Character>();
            var source = client.Character.Map.As<Map>();
            var destination = _mapManager.GetMapById(packet.MapId).As<Map>();

            if (!packet.IsJoining) return;

            if (source != null) source.RemoveEntity(character);

            destination.AddEntity(character);

            if (source != null) _pluginManager.CallEvent(new MapChangeEvent(client, source, destination));

            character.LastMapChange = DateTime.Now;
        }
    }
}