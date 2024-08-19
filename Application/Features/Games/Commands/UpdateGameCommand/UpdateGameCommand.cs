using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Games.Commands.UpdateGameCommand
{
    public class UpdateGameCommand : IRequest<UpdateGameCommandResponse>
    {
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Genres { get; set; } = Enumerable.Empty<string>();
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }

    }
}
