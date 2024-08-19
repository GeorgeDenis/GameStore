using MediatR;

namespace Application.Features.Games.Commands.DeleteGameCommand
{
    public class DeleteGameCommand : IRequest<DeleteGameCommandResponse>
    {
        public Guid GameId { get; set; }
    }
}
