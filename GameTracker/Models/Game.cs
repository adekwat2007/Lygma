using GameTracker.Enums;
using GameTracker.Models.Interfaces;

namespace GameTracker.Models
{
    internal class Game : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public Genre Genre { get; set; }
        public Platform Platform { get; set; }
        public CompletionStatus CompletionStatus { get; set; }
        public int PersonalRating { get; set; }
        public string GameCoverPath { get; set; }
    }
}