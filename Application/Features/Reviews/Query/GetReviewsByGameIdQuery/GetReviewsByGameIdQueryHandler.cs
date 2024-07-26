using Application.Models.Review;
using Application.Persistence;
using Mapster;
using MediatR;

namespace Application.Features.Reviews.Query.GetReviewsByGameIdQuery
{
    public class GetReviewsByGameIdQueryHandler : IRequestHandler<GetReviewsByGameIdQuery, GetReviewsByGameIdQueryResponse>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IGameRepository gameRepository;
        public GetReviewsByGameIdQueryHandler(IReviewRepository reviewRepository, IGameRepository gameRepository)
        {
            this.reviewRepository = reviewRepository; 
            this.gameRepository = gameRepository;
        }
        public async Task<GetReviewsByGameIdQueryResponse> Handle(GetReviewsByGameIdQuery request, CancellationToken cancellationToken)
        {
            var game = await gameRepository.FindByIdAsync(request.GameId);
            if (!game.IsSuccess)
            {
                return new GetReviewsByGameIdQueryResponse
                {
                    Success = false,
                    Message = "Game with this id doesn't exist"
                };
            }
            var reviews = await reviewRepository.GetReviewsByGameId(request.GameId);
            return new GetReviewsByGameIdQueryResponse
            {
                Success = true,
                Reviews = reviews.Value.Select(x => x.Adapt<GetReviewModel>()).ToList(),
            };
        }
    }
}
