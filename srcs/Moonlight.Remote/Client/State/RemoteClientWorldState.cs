using System;
using System.Threading;
using Moonlight.Remote.Cryptography;

namespace Moonlight.Remote.Client.State
{
    public class RemoteClientWorldState : RemoteClientState
    {
        private int _packetIdentifier;
        private Random rand = new Random();

        public RemoteClientWorldState(string ipAddress, int port, int encryptionKey)
            : base(ipAddress, port, new WorldCryptography(encryptionKey))
        {
            _packetIdentifier = rand.Next(50000, 55000);
            EncryptionKey = encryptionKey;
        }

        public int EncryptionKey { get; private set; }

        public override void SendPacket(string packet, bool session = false)
        {
            base.SendPacket(_packetIdentifier++ + " " + packet, session);
        }

        public virtual void Handshake(string accountName)
        {
            Thread.Sleep(100);
            SendPacket(EncryptionKey.ToString(), true);
            Thread.Sleep(100);
            SendPacket(accountName + " GF 7");
            Thread.Sleep(100);
            SendPacket("thisisgfmode");
        }
    }
}
