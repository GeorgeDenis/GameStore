using Application.Persistence;
using Domain.Common;
using Domain.Entities;
using NSubstitute;

namespace Application.Tests.Builders
{
    public class ReviewRepositoryMockBuilder : IBuilder<IReviewRepository>
    {
        private readonly IReviewRepository _mockReviewRepository = Substitute.For<IReviewRepository>();

        public IReviewRepository Build()
        {
            return _mockReviewRepository;
        }
        public ReviewRepositoryMockBuilder WithUserHasGameReturns(bool hasGame) { 
            _mockReviewRepository.UserHasGame(Arg.Any<Guid>(),Arg.Any<Guid>()).Returns(hasGame);
            return this;
        }
        public ReviewRepositoryMockBuilder WithAddReviewAsync(Result<Review> review)
        {
            _mockReviewRepository.AddAsync(Arg.Any<Review>()).Returns(review);
            return this;
        }

    }
}
