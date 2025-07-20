using GameTracker.Models;
using System.Net.Http;
using Newtonsoft.Json;  

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

        public async Task<GameResponse> GetGamesAsync(int page, int pageSize = 20)
        {
            var url = $"{_baseUrl}games?key={_apiKey}&page={page}&page_size={pageSize}";
            var json = await _httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<GameResponse>(json);

            return response ?? new();
        }

        public async Task<List<Genre>> GetGenresAsync()
        {
            var url = $"{_baseUrl}genres?key={_apiKey}";
            var json = await _httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<GenreResponse>(json);

            return response?.Results ?? new();
        }

        public async Task<List<Platform>> GetPlatformsAsync()
        {
            var url = $"{_baseUrl}platforms?key={_apiKey}";
            var json = await _httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<PlatformResponse>(json);

            return response?.Results ?? new();
        }

        public async Task<List<Developer>> GetDevelopersAsync()
        {
            var url = $"{_baseUrl}developers?key={_apiKey}";
            var json = await _httpClient.GetStringAsync(url);
            var response = JsonConvert.DeserializeObject<DeveloperResponse>(json);

            return response?.Results ?? new();
        }
    }
}