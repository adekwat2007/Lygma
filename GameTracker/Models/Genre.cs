using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace GameTracker.Models
{
    internal class Genre
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    internal class GenreResponse
    {
        [JsonPropertyName("results")]
        public List<Genre> Results { get; set; } = new();
    }
}