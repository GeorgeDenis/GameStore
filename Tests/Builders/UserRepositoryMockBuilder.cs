using Application.Persistence;
using Domain.Common;
using Domain.Entities;
using NSubstitute;

namespace Application.Tests.Builders
{
    public class UserRepositoryMockBuilder : IBuilder<IUserRepository>
    {
        private readonly IUserRepository _mockUserRepository = Substitute.For<IUserRepository>();

        public IUserRepository Build()
        {
            return _mockUserRepository;
        }

        public UserRepositoryMockBuilder WithFindByIdAsyncReturns(Result<User> user)
        {
            _mockUserRepository.FindByIdAsync(Arg.Any<Guid>()).Returns(user);
            return this;
        }
    }
}