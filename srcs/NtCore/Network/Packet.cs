﻿using System;
using System.Reflection;
using NtCore.Logging;

namespace NtCore.Network
{
    public abstract class Packet : IPacket
    {
        public virtual bool Deserialize(string[] packet)
        {
            Type type = GetType();

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                PacketIndex packetIndexAttribute = propertyInfo.GetCustomAttribute<PacketIndex>();
                if (packetIndexAttribute == null)
                {
                    continue;
                }

                int index = packetIndexAttribute.Value;
                if (index >= packet.Length)
                {
                    continue;
                }

                try
                {
                    propertyInfo.SetValue(this, Parse(packet[index], propertyInfo.PropertyType));
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        private static object Parse(string value, Type targetType)
        {
            if (targetType.BaseType == typeof(Enum))
            {
                if (targetType.IsEnumDefined(Enum.Parse(targetType, value)))
                {
                    return Enum.Parse(targetType, value);
                }
            }
            
            if (targetType == typeof(bool))
            {
                return value == "1";
            }

            return Convert.ChangeType(value, targetType);
        }
    }
}