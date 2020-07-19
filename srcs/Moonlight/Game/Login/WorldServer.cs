using System.Collections.Generic;

namespace Moonlight.Game.Login
{
    public class WorldServer
    {
        public WorldServer()
        {
            Channels = new List<Channel>();
        }
        
        public string WorldName { get; set; }
        
        public List<Channel> Channels { get; }
    }
}