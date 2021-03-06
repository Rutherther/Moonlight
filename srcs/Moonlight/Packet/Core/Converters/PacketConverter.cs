﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moonlight.Packet.Core.Attributes;
using Moonlight.Utility.Conversion;
using Moonlight.Utility.Conversion.Converters;

namespace Moonlight.Packet.Core.Converters
{
    internal class PacketConverter : Converter<IPacket>
    {
        private readonly IReflectionCache _reflectionCache;

        public PacketConverter(IReflectionCache reflectionCache) => _reflectionCache = reflectionCache;

        public override bool IsGeneric => true;

        protected override IPacket ToObject(string value, Type type, IConversionFactory factory)
        {
            CachedType cachedType = _reflectionCache.GetCachedType(type);
            if (cachedType == null)
            {
                throw new ConversionException(value, type);
            }

            int index = 0;
            foreach (PropertyData property in cachedType.Properties.Skip(1))
            {
                string separator = property.PacketIndexAttribute.Separator;
                int separatorIndex = value.IndexOf(separator, index, StringComparison.Ordinal);

                if (separator != " ")
                {
                    value = value.Remove(separatorIndex, separator.Length);
                    value = value.Insert(separatorIndex, " ");
                }

                index = separatorIndex + 1;
            }

            string[] split = value.Trim(cachedType.Properties.FirstOrDefault()?.PacketIndexAttribute?.Separator?.ToCharArray() ?? new[] { ' ' }).Split(' ');
            var packet = (IPacket)cachedType.Constructor.DynamicInvoke();

            foreach (PropertyData property in cachedType.Properties)
            {
                PacketIndexAttribute indexAttribute = property.PacketIndexAttribute;
                if (indexAttribute.Index >= split.Length)
                {
                    throw new ConversionException(value, type);
                }

                string content = split[indexAttribute.Index];
                if (indexAttribute.TillEnd)
                {
                    content = string.Join(" ", split.Skip(indexAttribute.Index));
                }

                if (property.PropertyType.GetInterfaces().Any(t => t.IsGenericType && 
                    t.GetGenericTypeDefinition() == typeof(IList<>)))
                {
                    content = content.Replace(property.PacketIndexAttribute.ListSeparator, " ");
                }

                var converted = factory.ToObject(content, property.PropertyType);
                if (converted is string str)
                {
                    converted = str.Replace("^", " ");
                }

                property.Setter.DynamicInvoke(packet, converted);
            }

            return packet;
        }

        protected override string ToString(IPacket value, Type type, IConversionFactory factory)
        {
            var output = new StringBuilder();
            CachedType cachedType = _reflectionCache.GetCachedType(type);

            if (cachedType == null)
            {
                throw new InvalidOperationException($"Unable to resolved packet {type.Name}");
            }

            foreach (PropertyData property in cachedType.Properties)
            {
                PacketIndexAttribute indexAttribute = property.PacketIndexAttribute;

                object obj = property.Getter.DynamicInvoke(value);
                string content = factory.ToString(obj, property.PropertyType);

                if (property.PacketIndexAttribute.Separator != null)
                {
                    content = content.Replace(" ", property.PacketIndexAttribute.Separator);
                }

                output.Append(content).Append(indexAttribute.Separator);
            }

            return output.ToString().TrimEnd();
        }
    }
}