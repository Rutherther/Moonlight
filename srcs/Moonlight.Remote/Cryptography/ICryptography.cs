using System.Collections.Generic;

namespace Moonlight.Remote.Cryptography
{
    public interface ICryptography
    {
        List<string> Decrypt(byte[] bytes, int size);

        byte[] Encrypt(string data);
        byte[] Encrypt(string data, bool session);
    }
}
