namespace Moonlight.Packet.Core.Serialization
{
    public interface ISerializer
    {
        /// <summary>
        ///     Serializes packet
        /// </summary>
        /// <param name="packet">Packet to serialize</param>
        /// <returns>Raw packet serialized</returns>
        string Serialize(IPacket packet);
    }
}