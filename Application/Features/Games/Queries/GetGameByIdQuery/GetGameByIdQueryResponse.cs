using Application.Models.Game;
using Application.Responses;

namespace Application.Features.Games.Queries.GetGameByIdQuery
{
    public class GetGameByIdQueryResponse : BaseResponse
    {
        public GetGameByIdQueryResponse() : base()
        {
            
        }
        public GetGameModel Game { get; set; }
    }
}