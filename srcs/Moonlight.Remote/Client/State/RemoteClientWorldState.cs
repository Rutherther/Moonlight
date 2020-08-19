using System;
using System.Threading;
using Moonlight.Core.Enums;
using Moonlight.Core.Logging;
using Moonlight.Remote.Cryptography;

namespace Moonlight.Remote.Client.State
{
    public class RemoteClientWorldState : RemoteClientState
    {
        private int _packetIdentifier;
        private Random rand = new Random();

        public RemoteClientWorldState(ILogger logger, RegionType region, string ipAddress, int port, int encryptionKey)
            : base(logger, ipAddress, port, new WorldCryptography(encryptionKey))
        {
            Region = region;
            _packetIdentifier = rand.Next(50000, 55000);
            EncryptionKey = encryptionKey;
        }

        public RegionType Region { get; set; }

        public int EncryptionKey { get; private set; }

        public override void SendPacket(string packet, bool session = false)
        {
            base.SendPacket(_packetIdentifier++ + " " + packet, session);
        }

        public virtual void Handshake(string accountName)
        {
            _logger.Debug("Sending world handshake with account {accountName}");
            Thread.Sleep(100);
            SendPacket(EncryptionKey.ToString(), true);
            Thread.Sleep(100);
            SendPacket(accountName + " GF " + (short)Region);
            Thread.Sleep(100);
            SendPacket("thisisgfmode");
        }
    }
}
