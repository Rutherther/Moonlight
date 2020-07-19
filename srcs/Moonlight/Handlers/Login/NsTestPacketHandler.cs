using System.Collections.Generic;
using System.Linq;
using Moonlight.Clients;
using Moonlight.Event;
using Moonlight.Event.Login;
using Moonlight.Game.Login;
using Moonlight.Packet.Login;

namespace Moonlight.Handlers.Login
{
    public class NsTestPacketHandler : PacketHandler<NsTestPacket>
    {
        private readonly IEventManager _eventManager;

        public NsTestPacketHandler(IEventManager eventManager) => _eventManager = eventManager;
        
        protected override void Handle(Client client, NsTestPacket packet)
        {
            IEnumerable<IGrouping<string, NsTeStSubPacket>> grouped = packet.NsTestSubPackets.GroupBy(x => x.Name);

            var servers = new List<WorldServer>();
            foreach (IGrouping<string, NsTeStSubPacket> grouping in grouped)
            {
                var server = new WorldServer
                {
                    WorldName = grouping.First().Name
                };

                foreach (NsTeStSubPacket subPacket in grouping)
                {
                    server.Channels.Add(new Channel
                    {
                        ChannelId = subPacket.WorldId,
                        Color = subPacket.Color ?? 0,
                        Host = subPacket.Host,
                        Port = subPacket.Port ?? 0
                    });
                }
                
                servers.Add(server);
                
                _eventManager.Emit(new ServersReceivedEvent(client)
                {
                    Servers = servers,
                    AccountName = packet.AccountName,
                    SessionId = packet.SessionId
                });
            }
        }
    }
}