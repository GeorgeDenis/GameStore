using Application.Models.Game;
using Application.Models.Review;
using Application.Responses;

namespace Application.Features.Reviews.Query.GetReviewsByGameIdQuery
{
    public class GetReviewsByGameIdQueryResponse : BaseResponse
    {
        public GetReviewsByGameIdQueryResponse() : base()
        {
            
        }
        public List<GetReviewModel> Reviews { get; set; }
    }
}