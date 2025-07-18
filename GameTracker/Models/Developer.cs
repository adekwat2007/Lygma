using Newtonsoft.Json;

namespace GameTracker.Models
{
    internal class Developer
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}