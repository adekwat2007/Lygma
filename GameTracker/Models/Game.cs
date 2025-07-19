using Newtonsoft.Json;

namespace GameTracker.Models
{
    internal class Game
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("released")]
        public string ReleaseDate { get; set; }

        [JsonProperty("background_image")]
        public string ImageUrl { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }

        [JsonProperty("platforms")]
        public List<Platform> Platforms { get; set; }

        [JsonProperty("developers")]
        public List<Developer> Developers { get; set; }
    }
}