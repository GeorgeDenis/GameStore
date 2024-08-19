using Application.Features.Developers.Commands.CreateDeveloperCommand;
using Application.Persistence;

namespace Application.Tests.Builders
{
    public class CreateDeveloperCommandHandlerBuilder : IBuilder<CreateDeveloperCommandHandler>
    {
        private IDeveloperRepository _developerRepository = new DeveloperRepositoryMockBuilder().Build();
        public CreateDeveloperCommandHandler Build()
        {
            return new CreateDeveloperCommandHandler(_developerRepository);
        }
        public CreateDeveloperCommandHandlerBuilder WithDeveloperRepository(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
            return this;
        }
    }
}
