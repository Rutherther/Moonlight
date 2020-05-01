using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moonlight.Clients;
using Moonlight.Event;
using Moonlight.Event.Raids;
using Moonlight.Game.Raids;
using Moonlight.Packet.Raid;

namespace Moonlight.Handlers.Raids
{
    internal class RaidPacketHandler : PacketHandler<RaidPacket>
    {
        private readonly IEventManager _eventManager;

        public RaidPacketHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        protected override void Handle(Client client, RaidPacket packet)
        {
            Raid raid = client.Character.Raid;

            if (packet.Type == 2 && packet.Data == -1 && !raid.Ended)
            {
                raid.Status = RaidStatus.Left;
                _eventManager.Emit(new RaidStatusChangedEvent(client)
                {
                    Raid = raid
                });
            }
        }
    }
}
