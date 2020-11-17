using Moonlight.Clients;
using Moonlight.Game.Entities;
using Moonlight.Game.Maps;

namespace Moonlight.Event.Maps
{
    public class ItemPickedUpEvent : IEventNotification
    {
        public ItemPickedUpEvent(Client emitter) => Emitter = emitter;

        public Map Map { get; internal set; }
        
        public GroundItem Item { get; internal set; }
        
        public Entity Entity { get; internal set; }
        
        public Client Emitter { get; }
    }
}