using System.Security.Cryptography;
using System.Text;

namespace Moonlight.Remote.Cryptography
{
    public static class Cryptography
    {
        public static string ToMd5(string value)
        {
            using (var md5 = MD5.Create())
            {
                return Hash(value, md5);
            }
        }

        public static string ToSha512(string value)
        {
            using (var sha512 = SHA512.Create())
            {
                return Hash(value, sha512);
            }
        }
    
        private static string Hash(string value, HashAlgorithm hash)
        {
            byte[] bytes = hash.ComputeHash(Encoding.ASCII.GetBytes(value));
            var sb = new StringBuilder();
        
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}