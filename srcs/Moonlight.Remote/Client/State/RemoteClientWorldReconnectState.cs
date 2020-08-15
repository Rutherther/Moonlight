using System.Threading;
using Moonlight.Core.Logging;

namespace Moonlight.Remote.Client.State
{
    public class RemoteClientWorldReconnectState : RemoteClientWorldState
    {
        protected readonly byte _dacIdentifier;
        
        public RemoteClientWorldReconnectState(ILogger logger, byte dacIdentifier, string ipAddress, int port, int encryptionKey)
            : base(logger, ipAddress, port, encryptionKey)
        {
            _dacIdentifier = dacIdentifier;
        }

        public override void Handshake(string accountName)
        {
            Thread.Sleep(100);
            SendPacket(EncryptionKey.ToString(), true);
            Thread.Sleep(100);
            SendPacket($"DAC {accountName} {_dacIdentifier} 7");
        }
    }
}