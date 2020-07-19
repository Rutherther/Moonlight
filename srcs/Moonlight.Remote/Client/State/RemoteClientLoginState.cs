using Moonlight.Remote.Cryptography;

namespace Moonlight.Remote.Client.State
{
    public class RemoteClientLoginState : RemoteClientState
    {
        public RemoteClientLoginState(string ipAddress, int port, string dxHash, string glHash, string version)
            : base(ipAddress, port, new LoginCryptography(dxHash, glHash, version))
        {
            
        }
    }
}
