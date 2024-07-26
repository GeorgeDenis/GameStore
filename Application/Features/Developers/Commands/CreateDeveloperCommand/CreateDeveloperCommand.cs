using MediatR;

namespace Application.Features.Developers.Commands.CreateDeveloperCommand
{
    public class CreateDeveloperCommand : IRequest<CreateDeveloperCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
