using System;
using System.Collections.Generic;
using System.Text;
using Moonlight.Clients;
using Moonlight.Core.Enums;

namespace Moonlight.Game.Dialogs
{
    public class Dialog
    {
        public Dialog(Client client, string acceptCommand, string denyCommand = null)
        {
            Client = client;
            AcceptCommand = acceptCommand;
            DenyCommand = denyCommand;
        }

        public bool Pending { get; private set; } = true;

        public Client Client { get; }

        public string AcceptCommand { get; }

        public string DenyCommand { get; }

        public Game18NConstString Type { get; set; } = Game18NConstString.Undefined0;

        public string PlayerName { get; set; }

        public void Accept()
        {
            if (!Pending)
            {
                return;
            }

            if (AcceptCommand != null)
            {
                Client.SendPacket(AcceptCommand);
            }

            Pending = false;
        }

        public void Deny()
        {
            if (!Pending)
            {
                return;
            }

            if (DenyCommand != null)
            {
                Client.SendPacket(DenyCommand);
            }

            Pending = false;
        }
        
    }
}
