using Moonlight.Core.Enums;

namespace Moonlight.Game.Act4
{
    public class Act4Status
    {
        public short Percentage { get; set; }
        
        public Act4Mode Mode { get; set; }
        
        public long? CurrentTime { get; set; }
        
        public long? TotalTime { get; set; }
        
        public Act4Raid? Raid { get; set; }
    }
}