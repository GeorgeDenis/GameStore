using Application.Models.Game;
using Application.Responses;

namespace Application.Features.Games.Queries.GetGamesQuery
{
    public class GetGamesQueryResponse : BaseResponse
    {
        public GetGamesQueryResponse() : base()
        {
            
        }
        public List<GetGameModel> Games { get; set; }
    }
}