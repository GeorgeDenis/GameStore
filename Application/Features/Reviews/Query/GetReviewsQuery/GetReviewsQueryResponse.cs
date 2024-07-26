using Application.Models.Review;
using Application.Responses;

namespace Application.Features.Reviews.Query.GetReviewsQuery
{
    public class GetReviewsQueryResponse : BaseResponse
    {
        public GetReviewsQueryResponse() : base()
        {
            
        }
        public List<GetReviewModel> Reviews { get; set; }
    }
}