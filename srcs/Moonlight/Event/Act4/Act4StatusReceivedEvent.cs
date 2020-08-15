using Moonlight.Clients;
using Moonlight.Core.Enums;
using Moonlight.Game.Act4;
using Moonlight.Packet.Act4;
using Moonlight.Packet.Core.Attributes;

namespace Moonlight.Event.Act4
{
    public class Act4StatusReceivedEvent : IEventNotification
    {
        public Act4StatusReceivedEvent(Client emitter)
            => Emitter = emitter;
        
        public Client Emitter { get; }
        
        public FactionType Faction { get; set; }
        
        public long MinutesUntilReset { get; set; }
        
        public Act4Status AngelState { get; set; }

        public Act4Status DemonState { get; set; }
    }
}