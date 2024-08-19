using Application.Features.Games.Commands.CreateGameCommand;
using Application.Features.Reviews.Commands.CreateReviewCommand;
using Application.Hubs;
using Application.Persistence;
using Microsoft.AspNetCore.SignalR;
using NSubstitute;

namespace Application.Tests.Builders
{
    public class CreateGameCommandHandlerBuilder : IBuilder<CreateGameCommandHandler>
    {
        private IGameRepository _gameRepository = new GameRepositoryMockBuilder().Build();
        private IDeveloperRepository _developerRepository = new DeveloperRepositoryMockBuilder().Build();

        public CreateGameCommandHandler Build()
        {
            return new CreateGameCommandHandler(_gameRepository, _developerRepository);
        }
        public CreateGameCommandHandlerBuilder WithGameRepository(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
            return this;
        }
        public CreateGameCommandHandlerBuilder WithDeveloperRepository(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
            return this;
        }
    }
}
