using System.Threading;

namespace Moonlight.Remote.Client.State
{
    public class RemoteClientWorldReconnectState : RemoteClientWorldState
    {
        public RemoteClientWorldReconnectState(string ipAddress, int port, int encryptionKey) : base(ipAddress, port, encryptionKey)
        public RemoteClientWorldReconnectState(ILogger logger, byte dacIdentifier, string ipAddress, int port, int encryptionKey)
            : base(logger, ipAddress, port, encryptionKey)
        {
        }

        public override void Handshake(string accountName)
        {
            Thread.Sleep(100);
            SendPacket(EncryptionKey.ToString(), true);
            Thread.Sleep(100);
            SendPacket("DAC " + accountName + " 0 7");
        }
    }
}