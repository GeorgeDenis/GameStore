using Application.Models.Review;
using Application.Persistence;
using Mapster;
using MediatR;

namespace Application.Features.Reviews.Query.GetReviewByIdQuery
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, GetReviewByIdQueryResponse>
    {
        private readonly IReviewRepository reviewRepository;
        public GetReviewByIdQueryHandler(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }
        public async Task<GetReviewByIdQueryResponse> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var review = await reviewRepository.FindByIdAsync(request.ReviewId);
            if (!review.IsSuccess)
            {
                return new GetReviewByIdQueryResponse
                {
                    Success = false,
                    Message = "Review with this id doesn't exist"
                };
            }
            return new GetReviewByIdQueryResponse
            {
                Success = true,
                Review = review.Value.Adapt<GetReviewModel>()
            };
        }
    }
}
