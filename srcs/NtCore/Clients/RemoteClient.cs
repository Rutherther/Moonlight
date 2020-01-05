﻿using System;
using System.Threading.Tasks;
using NtCore.Clients.Remote.Network;
using NtCore.Enums;
using NtCore.Game.Entities;

namespace NtCore.Clients
{
    public class RemoteClient : IClient
    {
        private readonly INetworkClient _networkClient;

        public RemoteClient(INetworkClient networkClient)
        {
            Id = Guid.NewGuid();
            Character = new Character(this);
            Type = ClientType.REMOTE;

            _networkClient = networkClient;

            _networkClient.PacketReceived += packet => PacketReceived?.Invoke(packet);
        }

        public Guid Id { get; }
        public Character Character { get; }
        public ClientType Type { get; }

        public async Task SendPacket(string packet)
        {
            await _networkClient.SendPacket(packet);
            PacketSend?.Invoke(packet);
        }

        public Task ReceivePacket(string packet)
        {
            PacketReceived?.Invoke(packet);
            return Task.CompletedTask;
        }

        public event Func<string, bool> PacketSend;
        public event Func<string, bool> PacketReceived;

        public void Dispose()
        {
            _networkClient.Dispose();
        }

        public bool Equals(IClient other) => other != null && other.Id == Id;
    }
}