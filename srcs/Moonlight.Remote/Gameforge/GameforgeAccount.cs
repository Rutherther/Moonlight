using Newtonsoft.Json;

namespace Moonlight.Remote.Gameforge
{
    public sealed class GameforgeAccount
    {
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string Name { get; set; }

        public override string ToString() => $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
    }
}
