namespace Domain.Entities
{
    public class Review
    {
        public Review()
        {

        }

        public Review(int rating, string comment, Guid userId, Guid gameId, User user, Game game)
        {
            ReviewId = Guid.NewGuid();
            Rating = rating;
            Comment = comment;
            ReviewDate = DateTime.UtcNow;
            UserId = userId;
            GameId = gameId;
            User = user;
            Game = game;
        }
        public Guid ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        public Guid UserId { get; set; }
        public Guid GameId { get; set; }


        public User User { get; set; }
        public Game Game { get; set; }
    }
}
