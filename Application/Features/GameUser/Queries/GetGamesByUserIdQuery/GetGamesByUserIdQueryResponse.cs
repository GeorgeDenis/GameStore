using Application.Models.Game;
using Application.Responses;

namespace Application.Features.GameUser.Queries.GetGamesByUserIdQuery
{
    public class GetGamesByUserIdQueryResponse : BaseResponse
    {
        public GetGamesByUserIdQueryResponse() : base()
        {
            
        }
        public List<GetGameModel> Games { get; set; }
    }
}