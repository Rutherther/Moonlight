using Moonlight.Clients;
using Moonlight.Core.Enums;

namespace Moonlight.Event.Login
{
    public class LoginFailEvent : IEventNotification
    {
        public LoginFailEvent(Client emitter)
            => Emitter = emitter;
        
        public Client Emitter { get; }
        
        public LoginFailType Type { get; set; }
    }
}