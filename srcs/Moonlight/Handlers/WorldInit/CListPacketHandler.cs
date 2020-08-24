using System.Collections.Generic;
using Moonlight.Clients;
using Moonlight.Core.Logging;
using Moonlight.Game.Entities;
using Moonlight.Packet.WorldInit;

namespace Moonlight.Handlers.WorldInit
{
    public class CListPacketHandler : PacketHandler<CListPacket>
    {
        public static readonly string CListPacketCacheKey = "CListData";

        private readonly ILogger _logger;
        private readonly IPacketHandlerCache _cache;

        public CListPacketHandler(ILogger logger, IPacketHandlerCache cache)
        {
            _logger = logger;
            _cache = cache;
        }
        
        protected override void Handle(Client client, CListPacket packet)
        {
            var character = new Character(_logger, 0, packet.Name, client)
            {
                Class = packet.Class,
                Gender = packet.Gender,
                Level = packet.Level
            };

            Dictionary<short, Character> cachedCharacters = _cache.Get<Dictionary<short, Character>>(CListPacketCacheKey);
            if (cachedCharacters == null)
            {
                cachedCharacters = new Dictionary<short, Character>();
            }
            
            cachedCharacters.Add(packet.Slot, character);
            
            _cache.Set(CListPacketCacheKey, cachedCharacters);
        }
    }
}