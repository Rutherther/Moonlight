using Moonlight.Clients;
using Moonlight.Game.Raids;

namespace Moonlight.Event.Raids
{
    public class RaidInfoRetrievedEvent : IEventNotification
    {
        public RaidInfoRetrievedEvent(Client emitter) => Emitter = emitter;

        public Raid Raid { get; set; }

        public Client Emitter { get; }
    }
}
