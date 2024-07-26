using Application.Persistence;
using MediatR;

namespace Application.Features.GameUser.Commands.AddGameUserCommand
{
    public class AddGameUserCommandHandler : IRequestHandler<AddGameUserCommand, AddGameUserCommandResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IGameRepository gameRepository;

        public AddGameUserCommandHandler(IUserRepository userRepository, IGameRepository gameRepository)
        {
            this.userRepository = userRepository;
            this.gameRepository = gameRepository;
        }

        public async Task<AddGameUserCommandResponse> Handle(AddGameUserCommand request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.FindByIdWithUsersAsync(request.GameId);
            if (!game.IsSuccess)
            {
                return new AddGameUserCommandResponse
                {
                    Success = false,
                    Message = "Game with this id doesn't exist"
                };
            }
            var user = await userRepository.FindByIdWithGamesAsync(request.UserId);
            if (!user.IsSuccess)
            {
                return new AddGameUserCommandResponse
                {
                    Success = false,
                    Message = "User with this id doesn't exist"
                };
            }
            var gameAlreadyOwned = user.Value.Games.FirstOrDefault(x => x.GameId == request.GameId);
            if(gameAlreadyOwned != null)
            {
                return new AddGameUserCommandResponse
                {
                    Success = false,
                    Message = "You already own this game!"
                };
            }
            user.Value.Games.Add(game.Value);
            var response = await userRepository.UpdateAsync(user.Value);
            if (!response.IsSuccess)
            {
                return new AddGameUserCommandResponse
                {
                    Success = false,
                    Message = "Adding a game to this user failed!"
                };
            }
            return new AddGameUserCommandResponse
            {
                Success = true,
            };
        }

    }

}
