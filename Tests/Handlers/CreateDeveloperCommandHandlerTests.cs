using Application.Features.Developers.Commands.CreateDeveloperCommand;
using Application.Tests.Builders;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;

namespace Application.Tests.Handlers
{
    public class CreateDeveloperCommandHandlerTests
    {
        private readonly DeveloperRepositoryMockBuilder _developerRepositoryMockBuilder;
        private readonly CreateDeveloperCommandHandlerBuilder _createDeveloperCommandHandlerBuilder;
        private readonly CreateDeveloperCommand _createDeveloperCommand = new CreateDeveloperCommand
        {
            Name = "Test",
            Description = "Test",
        };
        private readonly Developer developerTest = new Developer("Test","Test");

        private CreateDeveloperCommandHandler Sut => _createDeveloperCommandHandlerBuilder
            .WithDeveloperRepository(_developerRepositoryMockBuilder.Build())
            .Build();

        public CreateDeveloperCommandHandlerTests()
        {
            _developerRepositoryMockBuilder = new DeveloperRepositoryMockBuilder();
            _createDeveloperCommandHandlerBuilder = new CreateDeveloperCommandHandlerBuilder();
        }
        [Fact]
        public async Task Handle_ShouldReturnCreateDeveloperCommandResponseWithSuccessEqualFalse_WhenAddingDeveloperFails()
        {
            // Arrange
            _developerRepositoryMockBuilder.WithAddDeveloperAsync(Result<Developer>.Failure(""));

            // Act
            var response = await Sut.Handle(_createDeveloperCommand, CancellationToken.None);

            // Assert
            response.Success.Should().BeFalse();
        }
        [Fact]
        public async Task Handle_ShouldReturnCreateDeveloperCommandResponseWithSuccessEqualTrue_WhenAllConditionsAreMet()
        {
            // Arrange
            _developerRepositoryMockBuilder.WithAddDeveloperAsync(Result<Developer>.Success(developerTest));

            // Act
            var response = await Sut.Handle(_createDeveloperCommand, CancellationToken.None);

            // Assert
            response.Success.Should().BeTrue();
        }
    }
}
