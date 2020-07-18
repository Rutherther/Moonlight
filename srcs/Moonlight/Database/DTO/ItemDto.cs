using Moonlight.Database.DAL;
using NosCore.Packets.Enumerations;

namespace Moonlight.Database.Dto
{
    internal class ItemDto : IDto<int>
    {
        public string NameKey { get; set; }
        public int Type { get; set; }
        public int SubType { get; set; }
        public PocketType BagType { get; set; }
        public string Data { get; set; }
        public int Id { get; set; }
    }
}