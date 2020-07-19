using System;
using System.Collections.Generic;

namespace Moonlight.Remote.Cryptography
{
    public class LoginCryptography : ICryptography
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        private readonly string _dxHash;
        private readonly string _glHash;
        private readonly string _version;

        /// <summary>
        ///     Create a new LoginEncryption instance
        /// </summary>
        /// <param name="dxHash">NostaleClientX.exe hash</param>
        /// <param name="glHash">NostaleClient hash</param>
        /// <param name="version">Version of the client</param>
        public LoginCryptography(string dxHash, string glHash, string version)
        {
            _dxHash = dxHash;
            _glHash = glHash;
            _version = version;
        }

        /// <summary>
        ///     Decrypt the raw packet (byte array) to a readable string
        /// </summary>
        /// <param name="bytes">Bytes to decrypt</param>
        /// <param name="size">Amount of byte to translate</param>
        /// <returns>Decrypted packet as string</returns>
        public List<string> Decrypt(byte[] bytes, int size)
        {
            string output = "";
            for (int i = 0; i < size; i++)
            {
                output += Convert.ToChar(bytes[i] - 0xF);
            }

            var list = new List<string>();
            list.Add(output);
            
            return list;
        }

        /// <summary>
        ///     Encrypt the string packet to byte array
        /// </summary>
        /// <param name="value">String to encrypt</param>
        /// <param name="session">Ignored</param>
        /// <returns>Encrypted packet as byte array</returns>
        public byte[] Encrypt(string value, bool session = false)
        {
            var output = new byte[value.Length + 1];
            for (int i = 0; i < value.Length; i++)
            {
                output[i] = (byte)((value[i] ^ 0xC3) + 0xF);
            }
            output[output.Length - 1] = 0xD8;
            return output;
        }

        public override string ToString()
        {
            return $"{nameof(_dxHash)}: {_dxHash}, {nameof(_glHash)}: {_glHash}, {nameof(_version)}: {_version}";
        }
    }
}
