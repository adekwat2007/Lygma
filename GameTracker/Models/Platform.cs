using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace GameTracker.Models
{
    internal class Platform
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    internal class PlatformResponse
    {
        [JsonPropertyName("results")]
        public List<Platform> Results { get; set; } = new();
    }
}