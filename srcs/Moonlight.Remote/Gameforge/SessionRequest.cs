using Newtonsoft.Json;

namespace Moonlight.Remote.Gameforge
{
    public sealed class SessionRequest
    {
        [JsonProperty("platformGameAccountId")]
        public string PlatformGameAccountId { get; set; }
    }
}
