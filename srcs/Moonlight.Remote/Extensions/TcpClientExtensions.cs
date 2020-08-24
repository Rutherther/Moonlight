using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Moonlight.Remote.Extensions
{
    public static class TcpClientExtensions
    {
        public static TcpState GetState(this TcpClient tcpClient)
        {
            if (tcpClient.Client.LocalEndPoint == null)
            {
                return TcpState.Unknown;
            }
            
            try
            {
                var endPoint = (IPEndPoint)tcpClient.Client.LocalEndPoint;
                IPAddress ipv4 = endPoint.Address;
                if (ipv4.IsIPv4MappedToIPv6)
                {
                    ipv4 = ipv4.MapToIPv4();
                }
                
                TcpConnectionInformation foo = IPGlobalProperties.GetIPGlobalProperties()
                    .GetActiveTcpConnections()
                    .SingleOrDefault(x => x.LocalEndPoint.Address.Equals(ipv4) && x.LocalEndPoint.Port == endPoint.Port);
                return foo != null ? foo.State : TcpState.Unknown;
            }
            catch (ObjectDisposedException e)
            {
                return TcpState.Closed;
            }
        }
    }
}