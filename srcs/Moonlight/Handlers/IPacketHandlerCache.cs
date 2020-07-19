using System.Collections.Generic;
using System.Threading;

namespace Moonlight.Handlers
{
    public interface IPacketHandlerCache
    {
        T Get<T>(string name);

        void Set<T>(string name, T value);

        void Remove(string name);
    }
}