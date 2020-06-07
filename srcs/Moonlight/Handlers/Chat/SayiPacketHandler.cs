using System;
using System.Collections.Generic;
using System.Text;
using Moonlight.Clients;
using Moonlight.Core.Enums;
using Moonlight.Core.Logging;
using Moonlight.Event;
using Moonlight.Event.Chat;
using Moonlight.Game.Factory;
using Moonlight.Packet.Chat;

namespace Moonlight.Handlers.Chat
{
    public class SayiPacketHandler : PacketHandler<SayiPacket>
    {
        private readonly IEventManager _eventManager;
        private readonly ILogger _logger;

        public SayiPacketHandler(ILogger logger, IEventManager eventManager)
        {
            _eventManager = eventManager;
            _logger = logger;
        }

        protected override void Handle(Client client, SayiPacket packet)
        {
            _eventManager.Emit(new InternationalMessageReceivedEvent(client)
            {
                MessageType = InternationalMessageType.Sayi,
                ChatType = packet.Type,
                Color = packet.Color,
                Id = packet.Id,
                Message = packet.Message,
                Params = new List<long>(new[]
                {
                    packet.Param1,
                    packet.Param2,
                    packet.Param3,
                    packet.Param4,
                    packet.Param5,
                })
            });
        }
    }
}
