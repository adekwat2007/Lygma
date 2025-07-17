using GameTracker.Enums;
using GameTracker.Models.Interfaces;
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
    }
}