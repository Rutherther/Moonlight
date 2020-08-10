using System.Diagnostics.Tracing;
using Moonlight.Event;
using Moonlight.Event.Login;
using Moonlight.Remote.Control;

namespace Moonlight.Remote.Listeners
{
    public class LoginFailListener : EventListener<LoginFailEvent>
    {
        private readonly NosTaleLogin _login;
        
        public LoginFailListener(NosTaleLogin login)
        {
            _login = login;
        }
        
        protected override void Handle(LoginFailEvent notification)
        {
            _login.OnLoginFailed(notification.Type);
        }
    }
}