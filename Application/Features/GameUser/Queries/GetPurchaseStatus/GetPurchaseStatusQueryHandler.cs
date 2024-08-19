using Application.Features.GameUser.Commands.AddGameUserCommand;
using Application.Features.GameUser.Queries.GetGamesByUserIdQuery;
using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.GameUser.Queries.GetPurchaseStatus
{
    public class GetPurchaseStatusQueryHandler : IRequestHandler<GetPurchaseStatusQuery, GetPurchaseStatusQueryResponse>
    {
        private readonly IGameRepository gameRepository;
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;

        public GetPurchaseStatusQueryHandler(IGameRepository gameRepository, ITokenService tokenService, IUserRepository userRepository)
        {
            this.gameRepository = gameRepository;
            this.tokenService = tokenService;
            this.userRepository = userRepository;
        }
        public async Task<GetPurchaseStatusQueryResponse> Handle(GetPurchaseStatusQuery request, CancellationToken cancellationToken)
        {
            var userId = tokenService.GetUserId();
            if (!userId.IsSuccess)
            {
                return new GetPurchaseStatusQueryResponse
                {
                    Success = false,
                    Message = userId.Value,
                };
            }
            var game = await gameRepository.FindByIdAsync(request.GameId);
            if (!game.IsSuccess)
            {
                return new GetPurchaseStatusQueryResponse
                {
                    Success = false,
                    Message = "Game with this id not found",
                };
            }
            var user = await userRepository.FindByIdWithGamesAsync(Guid.Parse(userId.Value));
            if (!user.IsSuccess)
            {
                return new GetPurchaseStatusQueryResponse
                {
                    Success = false,
                    Message = "User with this id doesn't exist"
                };
            }
            var gameAlreadyOwned = user.Value.Games.FirstOrDefault(x => x.GameId == request.GameId);
            if (gameAlreadyOwned != null)
            {
                return new GetPurchaseStatusQueryResponse
                {
                    Success = true,
                    Message = "You already own this game!"
                };
            }
            return new GetPurchaseStatusQueryResponse
            {
                Success = false,
            };

        }
    }
}
