using System;

namespace Moonlight.Remote.Control.Version
{
    [Serializable]
    public struct NostaleVersion
    {
        public long? BranchId { get; set; }

        public string GameforgeClientVersion { get; set; }

        public string NosTaleVersion { get; set; }

        public string NostaleClientHash { get; set; }

        public string NostaleClientXHash { get; set; }

        public string CombinedHash => Cryptography.Cryptography.ToMd5(NostaleClientXHash + NostaleClientHash);

        public override string ToString() => NosTaleVersion;
    }
}
