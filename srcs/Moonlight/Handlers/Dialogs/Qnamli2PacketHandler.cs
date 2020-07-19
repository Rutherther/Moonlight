using System;
using System.Collections.Generic;
using System.Text;
using Moonlight.Clients;
using Moonlight.Core.Enums;
using Moonlight.Event;
using Moonlight.Event.Dialogs;
using Moonlight.Game.Dialogs;
using Moonlight.Packet.Dialogs;

namespace Moonlight.Handlers.Dialogs
{
    public class Qnamli2PacketHandler : PacketHandler<Qnamli2Packet>
    {
        private readonly IEventManager _eventManager;

        public Qnamli2PacketHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }

        protected override void Handle(Client client, Qnamli2Packet packet)
        {
            var dialog = new Dialog(client, packet.Command)
            {
                Type = packet.Type
            };

            switch (packet.Type)
            {
                case Game18NConstString.HasInvitedToMiniland:
                    dialog.PlayerName = packet.Parameters[0];
                    break;
            }

            _eventManager.Emit(new OpenDialogEvent(client)
            {
                Dialog = dialog
            });
        }
    }
}
