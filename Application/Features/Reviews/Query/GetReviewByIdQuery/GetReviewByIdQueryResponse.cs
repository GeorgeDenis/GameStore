using Application.Models.Review;
using Application.Responses;

namespace Application.Features.Reviews.Query.GetReviewByIdQuery
{
    public class GetReviewByIdQueryResponse : BaseResponse
    {
        public GetReviewByIdQueryResponse() : base()
        {
            
        }
        public GetReviewModel Review { get; set; }
    }
}