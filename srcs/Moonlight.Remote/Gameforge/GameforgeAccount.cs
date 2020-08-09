using System;
using Moonlight.Core.Enums;
using Newtonsoft.Json;

namespace Moonlight.Remote.Gameforge
{
    public sealed class GameforgeAccount
    {
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string Name { get; set; }

        [JsonProperty("accountGroup")]
        public string Region { get; set; }

        public override string ToString() => $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";

        public RegionType GetRegionType()
        {
            return (RegionType) Enum.Parse(typeof(RegionType), Region.ToUpper());
        }
    }
}
