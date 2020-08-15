using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Moonlight.Remote.Extensions
{
    public static class TcpClientExtensions
    {
        public static TcpState GetState(this TcpClient tcpClient)
        {
            try
            {
                TcpConnectionInformation foo = IPGlobalProperties.GetIPGlobalProperties()
                    .GetActiveTcpConnections()
                    .SingleOrDefault(x => x.LocalEndPoint.Equals(tcpClient.Client.LocalEndPoint));
                return foo != null ? foo.State : TcpState.Unknown;
            }
            catch (ObjectDisposedException e)
            {
                return TcpState.Closed;
            }
        }
    }
}