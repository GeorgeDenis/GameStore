namespace Domain.Entities
{
    public class Game
    {
        public Game()
        {

        }

        public Game(string name, string description, DateTime releaseDate, string genre, decimal price, Guid developerId, byte[] image)
        {
            GameId = Guid.NewGuid();
            Name = name;
            Description = description;
            ReleaseDate = releaseDate;
            Genre = genre;
            Price = price;
            DeveloperId = developerId;
            Image = image;
            Reviews = [];
            Users = [];
        }

        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }

        public Guid DeveloperId { get; set; }
        public byte[] Image { get; set; }


        public Developer developer { get; set; }

        public List<Review> Reviews { get; set; }
        public List<User> Users { get; set; }
    }
}
