using Domain.Common;
using MediatR;

namespace Application.Features.Users.Commands.CreateUserCommand
{
    public class CreateUserCommand : IRequest<CreateUserCommandResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
