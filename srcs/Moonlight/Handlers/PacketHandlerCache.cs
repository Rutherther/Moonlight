using System.Collections.Generic;

namespace Moonlight.Handlers
{
    public class PacketHandlerCache : IPacketHandlerCache
    {
        private readonly Dictionary<string, object> _cache;

        public PacketHandlerCache()
        {
            _cache = new Dictionary<string, object>();
        }

        public T Get<T>(string name)
        {
            if (_cache.ContainsKey(name))
            {
                return (T)_cache[name];
            }

            return default;
        }

        public void Set<T>(string name, T value)
        {
            if (_cache.ContainsKey(name))
            {
                _cache[name] = value;
            }
            else
            {
                _cache.Add(name, value);
            }
        }

        public void Remove(string name)
        {
            if (_cache.ContainsKey(name))
            {
                _cache.Remove(name);
            }
        }
    }
}