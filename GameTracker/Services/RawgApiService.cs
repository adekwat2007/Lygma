using GameTracker.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;

namespace GameTracker.Services
{
    internal class GenreResponse
    {
        public List<Genre> Results { get; set; } = new();
    }

    internal class PlatformResponse
    {
        public List<PlatformWrapper> Results { get; set; } = new();
    }

    internal class DeveloperResponse
    {
        public List<Developer> Results { get; set; } = new();
    }

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

        public async Task<List<Genre>> GetGenresAsync(int limit)
        {
            var url = $"{_baseUrl}genres?key={_apiKey}&page_size={limit}";
            var response = await _httpClient.GetFromJsonAsync<GenreResponse>(url);

            return response?.Results ?? new();
        }
        public async Task<List<Platform>> GetPlatformsAsync()
        {
            var url = $"{_baseUrl}platforms/lists/parents?key={_apiKey}";
            var response = await _httpClient.GetFromJsonAsync<PlatformResponse>(url);

            return response?.Results.Select(r => r.Platform).ToList() ?? new();
        }
        public async Task<List<Developer>> GetDevelopersAsync()
        {
            var url = $"{_baseUrl}developers?key={_apiKey}";
            var response = await _httpClient.GetFromJsonAsync<DeveloperResponse>(url);

            return response?.Results ?? new();
        }
    }
}