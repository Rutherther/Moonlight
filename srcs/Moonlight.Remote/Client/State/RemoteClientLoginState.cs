using Moonlight.Core.Logging;
using Moonlight.Remote.Cryptography;

namespace Moonlight.Remote.Client.State
{
    public class RemoteClientLoginState : RemoteClientState
    {
        public RemoteClientLoginState(ILogger logger, string ipAddress, int port, string dxHash, string glHash, string version)
            : base(logger, ipAddress, port, new LoginCryptography(dxHash, glHash, version))
        {
            
        }
    }
}
