using Application.Models.Game;
using Application.Responses;

namespace Application.Features.Games.Queries.GetGamesPaginatedQuery;

public class GetGamesPaginatedQueryResponse : BaseResponse
{
    public GetGamesPaginatedQueryResponse() : base()
    {

    }
    public List<GetGameModel> Games { get; set; }

}