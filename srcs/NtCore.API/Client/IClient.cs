﻿using NtCore.API.Game.Entities;

namespace NtCore.API.Client
{
    public interface IClient
    {
        ICharacter Character { get; }

        void SendPacket(string packet);
        void ReceivePacket(string packet);
    }
}