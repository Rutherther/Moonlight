using System;
using System.Threading;
using Moonlight.Remote.Cryptography;

namespace Moonlight.Remote.Client.State
{
    public class RemoteClientWorldState : RemoteClientState
    {
        protected int _encryptionKey;
        
        private int _packetIdentifier;
        private Random rand = new Random();

        public RemoteClientWorldState(string ipAddress, int port, int encryptionKey)
            : base(ipAddress, port, new WorldCryptography(encryptionKey))
        {
            _packetIdentifier = rand.Next(50000, 55000);
            _encryptionKey = encryptionKey;
        }

        public override void SendPacket(string packet, bool session = false)
        {
            base.SendPacket(_packetIdentifier++ + " " + packet, session);
        }

        public void Handshake(string accountName)
        {
            Thread.Sleep(100);
            SendPacket(_encryptionKey.ToString(), true);
            Thread.Sleep(100);
            SendPacket(accountName + " GF 7");
            Thread.Sleep(100);
            SendPacket("thisisgfmode");
        }
    }
}
