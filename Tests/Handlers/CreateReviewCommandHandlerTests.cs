using Application.Features.Reviews.Commands.CreateReviewCommand;
using Application.Tests.Builders;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;

namespace Application.Tests.Handlers
{
    public class CreateReviewCommandHandlerTests
    {
        private readonly ReviewRepositoryMockBuilder _reviewRepositoryMockBuilder;
        private readonly UserRepositoryMockBuilder _userRepositoryMockBuilder;
        private readonly GameRepositoryMockBuilder _gameRepositoryMockBuilder;
        private readonly CreateReviewCommandHandlerBuilder _createReviewCommandHandlerBuilder;
        private readonly CreateReviewCommand _createReviewCommand = new CreateReviewCommand { Rating = 4, Comment = "Test", UserId = Guid.NewGuid(), GameId = Guid.NewGuid() };
        private readonly User userTest = new User("Test", "test@yahoo.com", "Test1234@", Role.User);
        private readonly Game gameTest = new Game("Test", "Test", DateTime.Now, "Test", 0, Guid.NewGuid(), null);
        private readonly Review reviewTest = new Review(0, "Test", Guid.NewGuid(), Guid.NewGuid(), null, null);
        private CreateReviewCommandHandler Sut => _createReviewCommandHandlerBuilder
            .WithReviewRepository(_reviewRepositoryMockBuilder.Build())
            .WithUserRepository(_userRepositoryMockBuilder.Build())
            .WithGameRepository(_gameRepositoryMockBuilder.Build())
            .Build();  // system under test

        public CreateReviewCommandHandlerTests()
        {
            _reviewRepositoryMockBuilder = new ReviewRepositoryMockBuilder();
            _userRepositoryMockBuilder = new UserRepositoryMockBuilder();
            _gameRepositoryMockBuilder = new GameRepositoryMockBuilder();
            _createReviewCommandHandlerBuilder = new CreateReviewCommandHandlerBuilder();
        }

        //numeMetoda_ShouldDoSomething_WhenCondition
        [Fact]
        public async Task Handle_ShouldReturnCreateReviewCommandResponseWithSuccessEqualFalse_WhenUserIsNotFound()
        {
            // Arrange
            _userRepositoryMockBuilder.WithFindByIdAsyncReturns(Result<User>.Failure(""));

            // Act
            var response = await Sut.Handle(_createReviewCommand, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_ShouldReturnCreateReviewCommandResponseWithSuccessEqualFalse_WhenGameIsNotFound()
        {
            // Arrange
            _userRepositoryMockBuilder.WithFindByIdAsyncReturns(Result<User>.Success(userTest));
            _gameRepositoryMockBuilder.WithFindByIdAsyncReturns(Result<Game>.Failure(""));

            // Act
            var response = await Sut.Handle(_createReviewCommand, CancellationToken.None);

            //Assert
            response.Success.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_ShouldReturnCreateReviewCommandResponseWithSuccessEqualFalse_WhenReviewIsFound()
        {
            // Arrange
            _userRepositoryMockBuilder.WithFindByIdAsyncReturns(Result<User>.Success(userTest));
            _gameRepositoryMockBuilder.WithFindByIdAsyncReturns(Result<Game>.Success(gameTest));
            _reviewRepositoryMockBuilder.WithUserHasGameReturns(true);


            // Act
            var response = await Sut.Handle(_createReviewCommand, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_ShouldReturnCreateReviewCommandResponseWithSuccessEqualFalse_WhenReviewAddingFailed()
        {
            // Arrange
            _userRepositoryMockBuilder.WithFindByIdAsyncReturns(Result<User>.Success(userTest));
            _gameRepositoryMockBuilder.WithFindByIdAsyncReturns(Result<Game>.Success(gameTest));
            _reviewRepositoryMockBuilder.WithUserHasGameReturns(false);
            _reviewRepositoryMockBuilder.WithAddReviewAsync(Result<Review>.Failure(""));


            // Act
            var response = await Sut.Handle(_createReviewCommand, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
        }
        [Fact]
        public async Task Handle_ShouldReturnCreateReviewCommandResponseWithSuccessEqualTrue_WhenAllConditionsAreMet()
        {
            // Arrange
            _userRepositoryMockBuilder.WithFindByIdAsyncReturns(Result<User>.Success(userTest));
            _gameRepositoryMockBuilder.WithFindByIdAsyncReturns(Result<Game>.Success(gameTest));
            _reviewRepositoryMockBuilder.WithUserHasGameReturns(false);
            _reviewRepositoryMockBuilder.WithAddReviewAsync(Result<Review>.Success(reviewTest));


            // Act
            var response = await Sut.Handle(_createReviewCommand, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }
    }
}
