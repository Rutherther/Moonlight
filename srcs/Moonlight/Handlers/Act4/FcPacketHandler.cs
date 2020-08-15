using Moonlight.Clients;
using Moonlight.Core.Enums;
using Moonlight.Event;
using Moonlight.Event.Act4;
using Moonlight.Game.Act4;
using Moonlight.Packet.Act4;

namespace Moonlight.Handlers.Act4
{
    public class FcPacketHandler : PacketHandler<FcPacket>
    {
        private readonly IEventManager _eventManager;

        public FcPacketHandler(IEventManager eventManager)
        {
            _eventManager = eventManager;
        }
        
        protected override void Handle(Client client, FcPacket packet)
        {
            _eventManager.Emit(new Act4StatusReceivedEvent(client)
            {
                Faction = packet.Faction,
                MinutesUntilReset = packet.MinutesUntilReset,
                AngelState = GetStatus(packet.AngelState),
                DemonState = GetStatus(packet.DemonState),
            });
        }

        protected Act4Status GetStatus(FactionFcSubPacket fcSubPacket)
        {
            Act4Raid? raid = GetRaid(fcSubPacket);
            bool raidStarted = raid != null;
            
            return new Act4Status
            {
                Mode = fcSubPacket.Mode,
                Percentage = fcSubPacket.Percentage,
                CurrentTime = raidStarted ? fcSubPacket.CurrentTime : (long?)null,
                TotalTime = raidStarted ? fcSubPacket.TotalTime : (long?)null,
                Raid = raid
            };
        }

        protected Act4Raid? GetRaid(FactionFcSubPacket fcSubPacket)
        {
            if (fcSubPacket.IsMorcos)
            {
                return Act4Raid.Morcos;
            }

            if (fcSubPacket.IsCalvina)
            {
                return Act4Raid.Calvina;
            }

            if (fcSubPacket.IsBerios)
            {
                return Act4Raid.Berios;
            }

            if (fcSubPacket.IsHatus)
            {
                return Act4Raid.Hatus;
            }

            return null;
        }
    }
}