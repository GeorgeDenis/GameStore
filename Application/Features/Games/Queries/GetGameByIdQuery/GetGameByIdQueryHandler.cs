using Application.Models.Game;
using Application.Persistence;
using Application.Strategy;
using Mapster;
using MediatR;

namespace Application.Features.Games.Queries.GetGameByIdQuery
{
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GetGameByIdQueryResponse>
    {
        private readonly IGameRepository gameRepository;
        private readonly IPriceCalculator priceCalculator;
        public GetGameByIdQueryHandler(IGameRepository gameRepository, IPriceCalculator priceCalculator)
        {
            this.gameRepository = gameRepository;
            this.priceCalculator = priceCalculator;
        }
        public async Task<GetGameByIdQueryResponse> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.FindByIdAsync(request.GameId);
            if (!game.IsSuccess)
            {
                return new GetGameByIdQueryResponse
                {
                    Success = false,
                    Message = "Game with this id doesn't exist!"
                };
            }

            return new GetGameByIdQueryResponse
            {
                Success = true,
                Game = new GetGameModel
                {
                    GameId = request.GameId,
                    Name = game.Value.Name,
                    Description = game.Value.Description,
                    ReleaseDate = game.Value.ReleaseDate,
                    Price = priceCalculator.Calculate(game.Value.Price),
                    DeveloperId = game.Value.DeveloperId,
                    Image = Convert.ToBase64String(game.Value.Image),
                    Genre = ConvertFromGenresStringToArray(game.Value.Genre),
                }
            };
        }
        public static string[] ConvertFromGenresStringToArray(string genres)
        {
            return genres.Split(',');
        }
    }
}
