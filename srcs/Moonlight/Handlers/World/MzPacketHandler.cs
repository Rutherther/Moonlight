using Moonlight.Clients;
using Moonlight.Event;
using Moonlight.Event.World;
using Moonlight.Packet.World;

namespace Moonlight.Handlers.World
{
    public class MzPacketHandler : PacketHandler<MzPacket>
    {
        private IEventManager _eventManager;
        
        public MzPacketHandler(IEventManager eventManager)
            => _eventManager = eventManager;

        protected override void Handle(Client client, MzPacket packet)
        {
            _eventManager.Emit(new ServerChangeEvent(client)
            {
                Ip = packet.Ip,
                Port = packet.Port
            });
        }
    }
}