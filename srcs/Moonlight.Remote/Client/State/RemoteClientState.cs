using System;
using System.Net.Sockets;
using System.Threading;
using Moonlight.Remote.Cryptography;

namespace Moonlight.Remote.Client.State
{
    public class RemoteClientState : IState
    {
        public event Action<string> PacketReceived;
        private byte[] _buffer;

        public RemoteClientState(string ipAddress, int port, ICryptography cryptography)
        {
            _buffer = new byte[4 * 1024];

            Port = port;
            IpAddress = ipAddress;
            Cryptography = cryptography;
            Tcp = new TcpClient();
        }

        public Thread Thread { get; private set; }

        public TcpClient Tcp { get; }

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

        public void Disconnnect()
        {
            if (Tcp.Connected)
            {
                Tcp.Close();
            }
        }

        public virtual void SendPacket(string packet, bool session = false)
        {
            byte[] encrypted = Cryptography.Encrypt(packet, session);
            Tcp.GetStream().Write(encrypted, 0, encrypted.Length);
        }

        public virtual void ReceivePacket(string packet)
        {
            PacketReceived?.Invoke(packet);
        }

        protected void ThreadProc(object state)
        {
            NetworkStream stream = Tcp.GetStream();

            while (Tcp.Connected)
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
        }
    }
}