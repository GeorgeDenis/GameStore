namespace Application.Models.Game
{
    public class GetGameModel
    {
        public Guid GameId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<string> Genre { get; set; }
        public decimal Price { get; set; }

        public Guid DeveloperId { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}
