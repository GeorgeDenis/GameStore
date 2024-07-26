using Microsoft.AspNetCore.Http;

namespace Application.Models.Game
{
    public class PostGameModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }

        public Guid DeveloperId { get; set; }
        public IFormFile Image { get; set; }
        public IEnumerable<string> Genres { get; set; } 
    }
}
