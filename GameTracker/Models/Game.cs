using GameTracker.Enums;
using GameTracker.Models.Interfaces;

namespace GameTracker.Models
{
    internal class Game : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Platform> Platforms { get; set; }
        public CompletionStatus CompletionStatus { get; set; }
        public int PersonalRating { get; set; }
        public string GameCoverPath { get; set; }
    }
}