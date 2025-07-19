using Newtonsoft.Json;

namespace GameTracker.Models
{
    internal class Platform
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}