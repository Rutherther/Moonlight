using System;
using System.Reflection;
using Moonlight.Core.Logging;
using Moonlight.Packet.Core.Attributes;
using Moonlight.Utility.Conversion;

namespace Moonlight.Packet.Core.Serialization
{
    public class Serializer : ISerializer
    {
        private readonly IConversionFactory _conversionFactory;
        private readonly ILogger _logger;

        public Serializer(ILogger logger, IConversionFactory conversionFactory)
        {
            _conversionFactory = conversionFactory;
            _logger = logger;
        }

        public string Serialize(IPacket packet)
        {
            string header = packet.GetType().GetCustomAttribute<PacketHeaderAttribute>()?.Header;

            if (header == null)
            {
                _logger.Error($"Could not find header for {packet.GetType().FullName}");
                return null;
            }
            
            string content = _conversionFactory.ToString(packet, packet.GetType());

            _logger.Debug($"Successfully serialized {packet.GetType().FullName}");
            return header + " " + content;
        }
    }
}