using Moonlight.Clients;
using Moonlight.Packet.WorldInit;

namespace Moonlight.Handlers.WorldInit
{
    public class CListStartPacketHandler : PacketHandler<CListStartPacket>
    {
        private readonly IPacketHandlerCache _cache;
        
        public CListStartPacketHandler(IPacketHandlerCache cache)
        {
            _cache = cache;
        }
        
        protected override void Handle(Client client, CListStartPacket packet)
        {
            _cache.Remove(CListPacketHandler.CListPacketCacheKey);
        }
    }
}