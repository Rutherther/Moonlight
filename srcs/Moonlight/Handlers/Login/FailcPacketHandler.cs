using Moonlight.Clients;
using Moonlight.Event;
using Moonlight.Event.Login;
using Moonlight.Packet.Login;

namespace Moonlight.Handlers.Login
{
    public class FailcPacketHandler : PacketHandler<FailcPacket>
    {
        private readonly IEventManager _eventManager;

        public FailcPacketHandler(IEventManager eventManager) => _eventManager = eventManager;
        protected override void Handle(Client client, FailcPacket packet)
        {
            _eventManager.Emit(new LoginFailEvent(client)
            {
                Type = packet.Type
            });
        }
    }
}