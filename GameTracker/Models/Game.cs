using System.Text.Json.Serialization;

namespace GameTracker.Models
{
    internal class Game
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("background_image")]
        public string Background_Image { get; set; }

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }

        [JsonPropertyName("platforms")]
        public List<GamePlatformResponse> Platforms { get; set; }
    }

    internal class GameResponse
    {
        [JsonPropertyName("results")]
        public List<Game> Results { get; set; } = new();

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("previous")]
        public string Previous { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }

    internal class GamePlatformResponse
    {
        [JsonPropertyName("platform")]
        public Platform Platform { get; set; }
    }
}