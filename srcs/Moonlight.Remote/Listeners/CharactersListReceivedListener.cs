using Moonlight.Event;
using Moonlight.Event.WorldInit;
using Moonlight.Remote.Control;

namespace Moonlight.Remote.Listeners
{
    public class CharactersListReceivedListener : EventListener<CharactersListReceivedEvent>
    {
        private readonly NostaleWorld _world;

        public CharactersListReceivedListener(NostaleWorld world)
        {
            _world = world;
        }
        
        protected override void Handle(CharactersListReceivedEvent notification)
        {
            _world.OnCharacterListReceived(notification.Characters);
        }
    }
}