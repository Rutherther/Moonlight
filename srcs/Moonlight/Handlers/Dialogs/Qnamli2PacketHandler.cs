using Moonlight.Clients;
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
            var dialog = new Dialog(client, packet.Command);

            switch (packet.Type)
            {
                case 312:
                    dialog.Type = DialogType.MINILAND_INVITE;
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
