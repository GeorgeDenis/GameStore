using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.Reviews.Commands.CreateReviewCommand
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, CreateReviewCommandResponse>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IUserRepository userRepository;
        private readonly IGameRepository gameRepository;

        public CreateReviewCommandHandler(IReviewRepository reviewRepository, IUserRepository userRepository, IGameRepository gameRepository)
        {
            this.reviewRepository = reviewRepository;
            this.userRepository = userRepository;
            this.gameRepository = gameRepository;
        }
        public async Task<CreateReviewCommandResponse> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindByIdAsync(request.UserId);
            if (!user.IsSuccess)
            {
                return new CreateReviewCommandResponse
                {
                    Success = false,
                    Message = "User with this id doesn't exist"
                };
            }
            var game = await gameRepository.FindByIdAsync(request.GameId);
            if (!game.IsSuccess)
            {
                return new CreateReviewCommandResponse
                {
                    Success = false,
                    Message = "Game with this id doesn't exist"
                };
            }
            var reviewExists = await reviewRepository.UserHasGame(request.UserId, request.GameId);
            if (reviewExists)
            {
                return new CreateReviewCommandResponse
                {
                    Success = false,
                    Message = "You've already reviewed this game!"
                };
            }
            var review = new Review(request.Rating, request.Comment, request.UserId, request.GameId, user.Value, game.Value);
            var result = await reviewRepository.AddAsync(review);
            if (!result.IsSuccess)
            {
                return new CreateReviewCommandResponse
                {
                    Success = false,
                    Message = "Something went wrong while posting the review"
                };
            }
            return new CreateReviewCommandResponse
            {
                Success = true,
            };
        }
    }
}
