using Application.Features.Reviews.Commands.CreateReviewCommand;
using Application.Persistence;

namespace Application.Tests.Builders
{
    public class CreateReviewCommandHandlerBuilder : IBuilder<CreateReviewCommandHandler>
    {
        private IReviewRepository _reviewRepository = new ReviewRepositoryMockBuilder().Build();

        private IUserRepository _userRepository = new UserRepositoryMockBuilder().Build();
        private IGameRepository _gameRepository = new GameRepositoryMockBuilder().Build();
        public CreateReviewCommandHandler Build()
        {
            return new CreateReviewCommandHandler(_reviewRepository,_userRepository,_gameRepository);
        }

        public CreateReviewCommandHandlerBuilder WithReviewRepository(IReviewRepository reviewRepository) {
            _reviewRepository = reviewRepository;
            return this;
        }
        public CreateReviewCommandHandlerBuilder WithUserRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            return this;
        }
        public CreateReviewCommandHandlerBuilder WithGameRepository(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
            return this;
        }

    }
}
