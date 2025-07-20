using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GameTracker.Models
{
    internal class Developer
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
    internal class DeveloperResponse
    {
        [JsonPropertyName("results")]
        public List<Developer> Results { get; set; } = new();
    }
}