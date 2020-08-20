using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moonlight.Core.Enums;
using Moonlight.Packet.Dialogs;
using Moonlight.Utility.Conversion;
using Moonlight.Utility.Conversion.Converters;

namespace Moonlight.Packet.Core.Converters
{
    internal class Qnamli2PacketConverter : Converter<Qnamli2Packet>
    {
        protected override Qnamli2Packet ToObject(string value, Type type, IConversionFactory factory)
        {
            var packet = new Qnamli2Packet();
            string[] splitted = value.Split(' ');

            packet.Command = (string)factory.ToObject(splitted[1], typeof(string));
            packet.Type = (Game18NConstString)factory.ToObject(splitted[2], typeof(Game18NConstString));
            packet.ParametersCount = (int)factory.ToObject(splitted[3], typeof(int));


            packet.Parameters = new string[packet.ParametersCount];
            for (int i = 0; i < packet.ParametersCount; i++)
            {
                if (i + 4 >= splitted.Length)
                {
                    break;
                }
                
                packet.Parameters[i] = splitted[i + 4];
            }

            return packet;
        }

        protected override string ToString(Qnamli2Packet value, Type type, IConversionFactory factory) => throw new NotImplementedException();
    }
}
