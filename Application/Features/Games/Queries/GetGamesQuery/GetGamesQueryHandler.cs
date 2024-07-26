using Application.Models.Game;
using Application.Persistence;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Games.Queries.GetGamesQuery
{
    public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, GetGamesQueryResponse>
    {
        private readonly IGameRepository gameRepository;
        public GetGamesQueryHandler(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
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
