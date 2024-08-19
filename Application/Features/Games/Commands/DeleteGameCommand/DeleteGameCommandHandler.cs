using Application.Persistence;
using MediatR;

namespace Application.Features.Games.Commands.DeleteGameCommand
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, DeleteGameCommandResponse>
    {
        private readonly IGameRepository gameRepository;

        public DeleteGameCommandHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }
        public async Task<DeleteGameCommandResponse> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.FindByIdAsync(request.GameId);
            if (!game.IsSuccess)
            {
                return new DeleteGameCommandResponse
                {
                    Success = false,
                    Message = "Game with this id doesn't exist!",
                };
            }
            var response = await gameRepository.DeleteAsync(game.Value.GameId);
            if (!response.IsSuccess)
            {
                return new DeleteGameCommandResponse
                {
                    Success = false,
                    Message = "Error while trying to delete this game!"
                };
            }
            return new DeleteGameCommandResponse
            {
                Success = true,
            };

        }
    }
}
