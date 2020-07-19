using System;
using Moonlight.Packet.Raid;
using Moonlight.Utility.Conversion;
using Moonlight.Utility.Conversion.Converters;

namespace Moonlight.Packet.Core.Converters
{
    internal class RaidPacketConverter : Converter<RaidPacket>
    {
        protected override RaidPacket ToObject(string value, Type type, IConversionFactory factory)
        {
            string[] splitted = value.Split(' ');

            var packet = new RaidPacket
            {
                Type = RaidPacketType.Unknown
            };

            if (splitted[0] == "1")
            {
                if (splitted[1] == "0")
                {
                    packet.Type = RaidPacketType.Start;
                }

                if (splitted[1] == "1")
                {
                    packet.Type = RaidPacketType.Initialization;
                }
            }

            if (splitted[0] == "2")
            {
                if (splitted[1] == "-1")
                {
                    packet.Type = RaidPacketType.Left;
                }
                else
                {
                    packet.Type = RaidPacketType.RaidLeader;
                    packet.LeaderId = (long)factory.ToObject(splitted[1], typeof(long));
                }
            }

            if (splitted[0] == "3")
            {
                packet.Type = RaidPacketType.PlayerHealths;
            }



            return packet;
        }

        protected override string ToString(RaidPacket value, Type type, IConversionFactory factory) => throw new NotImplementedException();
    }
}
