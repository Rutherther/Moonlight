using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace Moonlight.Remote.Control.Version
{
    public class NostaleVersionRetriever
    {
        protected MD5 md5;
        
        public NostaleVersionRetriever(string nostalePath)
        {
            md5 = MD5.Create();
            
            NostalePath = nostalePath;
        }

        public string NostalePath { get; }

        public NostaleVersion GetVersion(int? branchId = null, string gameforgeVersion = "2.1.11")
        {
            return new NostaleVersion
            {
                BranchId = branchId,
                GameforgeClientVersion = gameforgeVersion,
                NostaleClientHash = GetNostaleClientHash(),
                NostaleClientXHash = GetNostaleClientXHash(),
                NosTaleVersion = GetNostaleVersion()
            };
        }

        public string GetNostaleClientHash()
        {
            using (FileStream stream = GetNostaleClient().OpenRead())
            {
                return BytesToString(md5.ComputeHash(stream));
            }
        }

        public string GetNostaleClientXHash()
        {
            using (FileStream stream = GetFile("NostaleClientX.exe").OpenRead())
            {
                return BytesToString(md5.ComputeHash(stream));
            }
        }

        public string GetNostaleVersion()
        {
            return FileVersionInfo.GetVersionInfo(GetNostaleClient().FullName).FileVersion;
        }

        protected FileInfo GetFile(string file)
        {
            return new FileInfo(Path.Combine(NostalePath, file));
        }

        protected FileInfo GetNostaleClient()
        {
            return GetFile("NostaleClient.exe");
        }
        
        protected string BytesToString(byte[] bytes)
        {
            string result = "";
            foreach (byte b in bytes) result += b.ToString("x2");
            return result.ToUpper();
        }
    }
}