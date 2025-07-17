using System.Net.Http;
using GameTracker.Models;
using Newtonsoft.Json.Linq;

namespace GameTracker.Services
{
    internal class RawgApiService
    {
        private readonly HttpClient _httpClient;
        private const string _baseUrl = "https://api.rawg.io/api/";
        private readonly string _apiKey = "497f50b6849b48458b2167f9f8e11546";

        public RawgApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Game>> SearchGamesAsync(string query)
        {
            var url = $"{_baseUrl}games?key={_apiKey}&search={Uri.EscapeDataString(query)}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);
            var results = json["results"]?.ToObject<List<Game>>() ?? new();

            return results;
        }
    }
}