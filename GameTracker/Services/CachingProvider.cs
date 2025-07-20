using GameTracker.Models;

namespace GameTracker.Services
{
    internal class CachingProvider
    {
        public List<Genre> Genres { get; set; }
        public List<Platform> Platforms { get; set; }
        public List<Developer> Developers { get; set; }
        public GameResponse Games { get; set; }
    }
}