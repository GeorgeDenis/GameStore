using Application.Models.Game;
using Application.Persistence;
using Application.Strategy;
using Mapster;
using MediatR;

namespace Application.Features.Games.Queries.GetGamesPaginatedQuery;

public class GetGamesPaginatedQueryHandler : IRequestHandler<GetGamesPaginatedQuery, GetGamesPaginatedQueryResponse>
{
    private readonly IGameRepository _gameRepository;
    private readonly IPriceCalculator _priceCalculator;

    public GetGamesPaginatedQueryHandler(IGameRepository gameRepository, IPriceCalculator priceCalculator)
    {
        _gameRepository = gameRepository;
        _priceCalculator = priceCalculator;
    }
    public async Task<GetGamesPaginatedQueryResponse> Handle(GetGamesPaginatedQuery request, CancellationToken cancellationToken)
    {
        var games = await _gameRepository.GetPagedResponseAsync(request.Page, request.Size);
        if (!games.IsSuccess)
        {
            return new GetGamesPaginatedQueryResponse
            {
                Success = false,
                Message = "An error occured during the request"
            };
        }
        return new GetGamesPaginatedQueryResponse
        {
            Success = true,
            Games = games.Value.Select(x => new GetGameModel
            {
                GameId = x.GameId,
                Name = x.Name,
                Description = x.Description,
                ReleaseDate = x.ReleaseDate,
                Price = _priceCalculator.Calculate(x.Price),
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
