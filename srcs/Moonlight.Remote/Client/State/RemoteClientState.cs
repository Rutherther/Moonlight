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
        public event Action<string> PacketSent;

        public event Action<Exception> Error;

        private byte[] _buffer;
        private bool _disconnectHandled;
        protected ILogger _logger;
        private Timer _timer;

        public RemoteClientState(ILogger logger, string ipAddress, int port, ICryptography cryptography)
        {
            _logger = logger;
            _buffer = new byte[4 * 1024];

            Port = port;
            IpAddress = ipAddress;
            Cryptography = cryptography;
            Tcp = new TcpClient();
        }

        public bool Connected => !_disconnectHandled && (Tcp?.GetState() ?? TcpState.Unknown) == TcpState.Established;
        
        public Thread Thread { get; private set; }

        public TcpClient Tcp { get; private set; }

        public ICryptography Cryptography { get; }

        public string IpAddress { get; }
        public int Port { get; }

        public void Connect()
        {
            if (!Connected)
            {
                Tcp.Connect(IpAddress, Port);
                Thread = new Thread(ThreadProc);
                Thread.Start();
                
                _timer = new Timer(CheckConnection, null, 1000, 500);
            }
        }

        public void Disconnnect(bool forced = false)
        {
            if (_disconnectHandled)
            {
                return;
            }
            
            _disconnectHandled = true;

            if (Tcp.Connected)
            {
                _logger.Error("Disconnecting the client");
                Tcp.Close();
                Tcp = null;
            }

            if (_timer != null)
            {
                _timer.Dispose();
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
                    PacketSent?.Invoke(packet);
                }
                catch (SocketException e)
                {
                    if (Tcp != null)
                    {
                        _logger.Error($"TCP state - {Tcp.GetState()}");
                    }
                    
                    _logger.Error(e);
                    Disconnnect();
                }
                catch (IOException e)
                {
                    if (Tcp != null)
                    {
                        _logger.Error($"TCP state - {Tcp.GetState()}");
                    }
                    
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
                    _logger.Error("There was an error");
                    _logger.Error(e);

                    if (Tcp != null)
                    {
                        _logger.Error($"TCP state - {Tcp.GetState()}");
                    }

                    if (!Connected)
                    {
                        Disconnnect();
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
            
            Disconnnect();
        }

        protected void CheckConnection(object state)
        {
            if (!Connected && !_disconnectHandled)
            {
                Disconnnect();
            }
        }
    }
}