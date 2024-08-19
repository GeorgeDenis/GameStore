using Application.Persistence;
using Domain.Common;
using Domain.Entities;
using NSubstitute;

namespace Application.Tests.Builders
{
    public class DeveloperRepositoryMockBuilder : IBuilder<IDeveloperRepository>
    {
        private readonly IDeveloperRepository _mockDeveloperRepository = Substitute.For<IDeveloperRepository>();

        public IDeveloperRepository Build()
        {
            return _mockDeveloperRepository;
        }

        public DeveloperRepositoryMockBuilder WithFindByIdAsyncReturns(Result<Developer> developer)
        {
            _mockDeveloperRepository.FindByIdAsync(Arg.Any<Guid>()).Returns(developer);
            return this;
        }

        public DeveloperRepositoryMockBuilder WithAddDeveloperAsync(Result<Developer> developer)
        {
            _mockDeveloperRepository.AddAsync(Arg.Any<Developer>()).Returns(developer);
            return this;
        }
    }
}
