using Application.Models.Game;
using Application.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.GameUser.Queries.GetGamesByUserIdQuery
{
    public class GetGamesByUserIdQueryHandler : IRequestHandler<GetGamesByUserIdQuery, GetGamesByUserIdQueryResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IGameRepository gameRepository;
        private readonly ITokenService tokenService;

        public GetGamesByUserIdQueryHandler(IUserRepository userRepository, IGameRepository gameRepository, ITokenService tokenService)
        {
            this.userRepository = userRepository;
            this.gameRepository = gameRepository;
            this.tokenService = tokenService;
        }
        public async Task<GetGamesByUserIdQueryResponse> Handle(GetGamesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userId = tokenService.GetUserId();
            if (!userId.IsSuccess)
            {
                return new GetGamesByUserIdQueryResponse
                {
                    Success = false,
                    Message = userId.Value,
                };
            }
            var user = await userRepository.FindByIdWithGamesAsync(Guid.Parse(userId.Value));
            if (!user.IsSuccess)
            {
                return new GetGamesByUserIdQueryResponse
                {
                    Success = false,
                    Message = "User with this id doesn't exist"
                };
            }
            return new GetGamesByUserIdQueryResponse
            {
                Success = true,
                Games = user.Value.Games.Select(x => new GetGameModel
                {
                    GameId = x.GameId,
                    Name = x.Name,
                    Description = x.Description,
                    ReleaseDate = x.ReleaseDate,
                    Price = x.Price,
                    DeveloperId = x.DeveloperId,
                    Image = Convert.ToBase64String(x.Image),
                    Genre = ConvertFromGenresStringToArray(x.Genre),

                }).ToList(),
            };
        }
        public static string[] ConvertFromGenresStringToArray(string genres)
        {
            return genres.Split(',');
        }
    }
}
