using Moonlight.Clients;
using Moonlight.Game.Entities;
using Moonlight.Game.Raids;

namespace Moonlight.Event.Raids
{
    public class RaidBossInitializedEvent : IEventNotification
    {
        public RaidBossInitializedEvent(Client emitter) => Emitter = emitter;

        public Raid Raid { get; internal set; }

        public Monster RaidBoss { get; set; }

        public Client Emitter { get; }
    }
}
