using System.Collections.Generic;
using Moonlight.Clients;
using Moonlight.Game.Entities;

namespace Moonlight.Event.WorldInit
{
    public class CharactersListReceivedEvent : IEventNotification
    {
        public CharactersListReceivedEvent(Client emitter)
            => Emitter = emitter;

        public Client Emitter { get; }
        
        public Dictionary<short, Character> Characters { get; set; }
    }
}