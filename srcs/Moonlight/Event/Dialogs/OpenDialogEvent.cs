using System;
using System.Collections.Generic;
using System.Text;
using Moonlight.Clients;
using Moonlight.Game.Dialogs;

namespace Moonlight.Event.Dialogs
{
    public class OpenDialogEvent : IEventNotification
    {
        public OpenDialogEvent(Client emitter) => Emitter = emitter;

        public Client Emitter { get; }

        public Dialog Dialog { get; set; }
    }
}
