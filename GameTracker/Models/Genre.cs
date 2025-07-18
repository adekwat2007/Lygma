using Newtonsoft.Json;

namespace GameTracker.Models
{
    internal class Genre
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}