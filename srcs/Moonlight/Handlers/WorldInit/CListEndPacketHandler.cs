using System.Collections.Generic;
using Moonlight.Clients;
using Moonlight.Core.Logging;
using Moonlight.Event;
using Moonlight.Event.WorldInit;
using Moonlight.Game.Entities;
using Moonlight.Packet.WorldInit;

namespace Moonlight.Handlers.WorldInit
{
    public class CListEndPacketHandler : PacketHandler<CListEndPacket>
    {
        private readonly ILogger _logger;
        private readonly IPacketHandlerCache _cache;
        private readonly IEventManager _eventManager;

        public CListEndPacketHandler(ILogger logger, IPacketHandlerCache cache, IEventManager eventManager)
        {
            _logger = logger;
            _cache = cache;
            _eventManager = eventManager;
        }
        
        protected override void Handle(Client client, CListEndPacket packet)
        {
            Dictionary<short, Character> cachedCharacters = _cache.Get<Dictionary<short, Character>>(CListPacketHandler.CListPacketCacheKey);

            _eventManager.Emit(new CharactersListReceivedEvent(client)
            {
                Characters = cachedCharacters
            });
        }
    }
}