using Moonlight.Clients;

namespace Moonlight.Event.World
{
    public class ServerChangeEvent : IEventNotification
    {
        public ServerChangeEvent(Client emitter)
            => Emitter = emitter;
        
        public Client Emitter { get; }
        
        public string Ip { get; set; }
        
        public short Port { get; set; }
    }
}