using Application.Persistence;
using Domain.Common;
using Domain.Entities;
using NSubstitute;

namespace Application.Tests.Builders
{
    public class GameRepositoryMockBuilder : IBuilder<IGameRepository>
    {
        private readonly IGameRepository _mockGameRepository = Substitute.For<IGameRepository>();
        public IGameRepository Build()
        {
            return _mockGameRepository;
        }

        public GameRepositoryMockBuilder WithFindByIdAsyncReturns(Result<Game> game)
        {
            _mockGameRepository.FindByIdAsync(Arg.Any<Guid>()).Returns(game);
            return this;
        }
        public GameRepositoryMockBuilder WithAddGameAsync(Result<Game> game)
        {
            _mockGameRepository.AddAsync(Arg.Any<Game>()).Returns(game);
            return this;
        }
    }
}
