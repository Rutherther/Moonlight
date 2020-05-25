using Moonlight.Clients;
using Moonlight.Local.Clients.Local;
using MoonlightC.Clients;
using MoonlightCore;

namespace Moonlight.Local.Clients
{
    public sealed class LocalClient : Client, ILocalClient
    {
        /// <summary>
        ///     Declared as private field to avoid GC
        /// </summary>
        private readonly NetworkCallback _recvCallback;

        private readonly NetworkCallback _sendCallback;

        public LocalClient(Window window)
        {
            Window = window;
            ManagedMoonlightCore = new ManagedMoonlightCore();

            _recvCallback = OnPacketReceived;
            _sendCallback = OnPacketSend;

            ManagedMoonlightCore.SetRecvCallback(_recvCallback);
            ManagedMoonlightCore.SetSendCallback(_sendCallback);
        }

        public Window Window { get; }

        internal ManagedMoonlightCore ManagedMoonlightCore { get; }

        public override void SendPacket(string packet)
        {
            ManagedMoonlightCore.SendPacket(packet);
        }

        public override void ReceivePacket(string packet)
        {
            ManagedMoonlightCore.ReceivePacket(packet);
        }
    }
}