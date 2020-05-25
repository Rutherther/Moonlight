using Moonlight.Clients;
using Moonlight.Game.Raids;

namespace Moonlight.Event.Raids
{
    public class RaidStatusChangedEvent : IEventNotification
    {
        public RaidStatusChangedEvent(Client emitter) => Emitter = emitter;

        public Raid Raid { get; set; }

        public Client Emitter { get; }
    }
}
