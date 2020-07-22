using System;
using System.Linq;
using System.Net.Http.Headers;

namespace Moonlight.Packet.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class PacketHeaderAttribute : Attribute
    {
        public PacketHeaderAttribute(string header) => Header = header;

        public PacketHeaderAttribute(params string[] headers)
        {
            Headers = headers;
            Header = Headers.First();
        }

        public string[] Headers { get; set; }

        public string Header { get; }
    }
}