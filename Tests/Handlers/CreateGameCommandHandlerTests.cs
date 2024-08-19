using Application.Features.Games.Commands.CreateGameCommand;
using Application.Features.Reviews.Commands.CreateReviewCommand;
using Application.Tests.Builders;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;

namespace Application.Tests.Handlers
{
    public class CreateGameCommandHandlerTests
    {
        private readonly GameRepositoryMockBuilder _gameRepositoryMockBuilder;
        private readonly DeveloperRepositoryMockBuilder _developerRepositoryMockBuilder;
        private readonly CreateGameCommandHandlerBuilder _createGameCommandHandlerBuilder;
        private readonly CreateGameCommand _createGameCommand = new CreateGameCommand
        {
            Name = "Test",
            Description = "Test",
            ReleaseDate = DateTime.UtcNow,
            Price = 0,
            DeveloperId = Guid.NewGuid(),
            Image = null,
            Genres = []
        };
        private readonly Developer developerTest = new Developer
        {
            Name = "Test",
            Description = "Test",
        };
        private readonly Game gameTest = new Game("Test", "Test", DateTime.Now, "Test", 0, Guid.NewGuid(), null);

        private CreateGameCommandHandler Sut => _createGameCommandHandlerBuilder
            .WithGameRepository(_gameRepositoryMockBuilder.Build())
            .WithDeveloperRepository(_developerRepositoryMockBuilder.Build())
            .Build();

        public CreateGameCommandHandlerTests()
        {
            _gameRepositoryMockBuilder = new GameRepositoryMockBuilder();
            _developerRepositoryMockBuilder = new DeveloperRepositoryMockBuilder();
            _createGameCommandHandlerBuilder = new CreateGameCommandHandlerBuilder();
        }
        [Fact]
        public async Task Handle_ShouldReturnCreateGameCommandResponseWithSuccessEqualFalse_WhenDeveloperNotFound()
        {
            // Arrange
            _developerRepositoryMockBuilder.WithFindByIdAsyncReturns(Result<Developer>.Failure(""));

            // Act
            var response = await Sut.Handle(_createGameCommand, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
        }
        [Fact]
        public async Task Handle_ShouldReturnCreateGameCommandResponseWithSuccessEqualFalse_WhenAddingDeveloperFails()
        {
            // Arrange
            _developerRepositoryMockBuilder.WithFindByIdAsyncReturns(Result<Developer>.Success(developerTest));
            _gameRepositoryMockBuilder.WithAddGameAsync(Result<Game>.Failure(""));

            // Act
            var response = await Sut.Handle(_createGameCommand, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
        }
        [Fact]
        public async Task Handle_ShouldReturnCreateGameCommandResponseWithSuccessEqualTrue_WhenAllConditionsAreMet()
        {
            // Arrange
            _developerRepositoryMockBuilder.WithFindByIdAsyncReturns(Result<Developer>.Success(developerTest));
            _gameRepositoryMockBuilder.WithAddGameAsync(Result<Game>.Success(gameTest));

            // Act
            var response = await Sut.Handle(_createGameCommand, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }
    }
}
