using Application.Models.Review;
using Application.Persistence;
using Mapster;
using MediatR;

namespace Application.Features.Reviews.Query.GetReviewsQuery
{
    public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, GetReviewsQueryResponse>
    {
        private readonly IReviewRepository reviewRepository;
        public GetReviewsQueryHandler(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public async Task<GetReviewsQueryResponse> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await reviewRepository.GetAllAsync();
            return new GetReviewsQueryResponse
            {
                Success = true,
                Reviews = reviews.Value.Select(x => x.Adapt<GetReviewModel>()).ToList(),
            };
        }
    }
}
