using Moonlight.Event;
using Moonlight.Event.Login;
using Moonlight.Remote.Control;

namespace Moonlight.Remote.Listeners
{
    public class ServersReceivedListener : EventListener<ServersReceivedEvent>
    {
        private readonly NosTaleLogin _login;
        
        public ServersReceivedListener(NosTaleLogin login)
        {
            _login = login;
        }
        
        protected override void Handle(ServersReceivedEvent notification)
        {
            _login.OnServersReceived(notification.SessionId, notification.AccountName, notification.Servers);
        }
    }
}