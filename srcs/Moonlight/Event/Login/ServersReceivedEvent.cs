using System.Collections.Generic;
using Moonlight.Clients;
using Moonlight.Game.Login;

namespace Moonlight.Event.Login
{
    public class ServersReceivedEvent : IEventNotification
    {
        public ServersReceivedEvent(Client emitter)
            => Emitter = emitter;
        
        public Client Emitter { get; }

        public string AccountName { get; set; }
        
        public int SessionId { get; set; }
        
        public List<WorldServer> Servers { get; set; }
    }
}