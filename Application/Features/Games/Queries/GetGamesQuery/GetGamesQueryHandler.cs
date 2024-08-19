using Application.Models.Game;
using Application.Persistence;
using Application.Strategy;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Games.Queries.GetGamesQuery
{
    public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, GetGamesQueryResponse>
    {
        private readonly IGameRepository gameRepository;
        private readonly IAppLogger<GetGamesQueryHandler> logger;
        private readonly IPriceCalculator priceCalculator;
        public GetGamesQueryHandler(IGameRepository gameRepository, IAppLogger<GetGamesQueryHandler> logger, IPriceCalculator priceCalculator)
        {
            this.gameRepository = gameRepository;
            this.logger = logger;
            this.priceCalculator = priceCalculator;
        }
        public async Task<GetGamesQueryResponse> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            var games = await gameRepository.GetAllAsync();
            return new GetGamesQueryResponse
            {
                Success = true,
                Games = games.Value.Select(x => new GetGameModel
                {
                    GameId = x.GameId,
                    Name = x.Name,
                    Description = x.Description,
                    ReleaseDate = x.ReleaseDate,
                    Price = priceCalculator.Calculate(x.Price),
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
