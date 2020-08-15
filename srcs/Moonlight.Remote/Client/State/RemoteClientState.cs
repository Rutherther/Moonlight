using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using Moonlight.Core.Logging;
using Moonlight.Remote.Cryptography;
using Moonlight.Remote.Extensions;

namespace Moonlight.Remote.Client.State
{
    public class RemoteClientState : IState
    {
        public event Action Disconnected;
        public event Action<string> PacketReceived;
        
        private byte[] _buffer;
        private bool _disconnectHandled;
        private ILogger _logger;

        public RemoteClientState(ILogger logger, string ipAddress, int port, ICryptography cryptography)
        {
            _logger = logger;
            _buffer = new byte[4 * 1024];

            Port = port;
            IpAddress = ipAddress;
            Cryptography = cryptography;
            Tcp = new TcpClient();
        }

        public bool Connected => (Tcp?.GetState() ?? TcpState.Unknown) == TcpState.Established;
        
        public Thread Thread { get; private set; }

        public TcpClient Tcp { get; private set; }

        public ICryptography Cryptography { get; }

        public string IpAddress { get; }
        public int Port { get; }

        public void Connect()
        {
            if (!Tcp.Connected)
            {
                Tcp.Connect(IpAddress, Port);
                Thread = new Thread(ThreadProc);
                Thread.Start();
            }
        }

        public void Disconnnect(bool forced = false)
        {
            _disconnectHandled = true;
            
            if (Tcp.Connected)
            {
                Tcp.Close();
                Tcp = null;
            }

            if (Thread != null)
            {
                Thread thread = Thread;
                Thread = null;

                Disconnected?.Invoke();
            }
        }

        public virtual void SendPacket(string packet, bool session = false)
        {
            if (!_disconnectHandled && Connected)
            {
                try
                {
                    byte[] encrypted = Cryptography.Encrypt(packet, session);
                    Tcp.GetStream().Write(encrypted, 0, encrypted.Length);
                }
                catch (SocketException e)
                {
                    _logger.Error(e);
                    Disconnnect();
                }
                catch (IOException e)
                {
                    _logger.Error(e);
                    Disconnnect();
                }
            }
        }

        public virtual void ReceivePacket(string packet)
        {
            PacketReceived?.Invoke(packet);
        }

        protected void ThreadProc(object state)
        {
            NetworkStream stream = Tcp.GetStream();

            while (!_disconnectHandled && Connected)
            {
                try
                {
                    int read = stream.Read(_buffer, 0, _buffer.Length);
                    foreach (string packet in Cryptography.Decrypt(_buffer, read))
                    {
                        if (string.IsNullOrEmpty(packet))
                        {
                            continue;
                        }

                        ReceivePacket(packet);
                    }
                }
                catch (Exception e)
                {
                    if (!Connected)
                    {
                        Disconnnect();
                    }
                    else
                    {
                        _logger.Error(e);
                        throw e;
                    }
                }
            }
            
            Disconnnect();
        }
    }
}