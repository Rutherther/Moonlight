using System.Collections.Generic;
using Moonlight.Clients;
using Moonlight.Core.Enums;

namespace Moonlight.Event.Chat
{
    public class InternationalMessageReceivedEvent : IEventNotification
    {
        public InternationalMessageReceivedEvent(Client emitter)
            => Emitter = emitter;

        public Client Emitter { get; }

        public InternationalMessageType MessageType { get; set; }

        public long ChatType { get; set; }

        public long Id { get; set; }

        public long Message { get; set; }

        public byte Color { get; set; }

        public List<long> Params { get; set; }
    }
}
