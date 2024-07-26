using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Games.Commands.CreateGameCommand
{
    public class CreateGameCommand : IRequest<CreateGameCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public Guid DeveloperId { get; set; }
        public IFormFile Image { get; set; }
        public IEnumerable<string> Genres { get; set; } = Enumerable.Empty<string>();

    }
}
