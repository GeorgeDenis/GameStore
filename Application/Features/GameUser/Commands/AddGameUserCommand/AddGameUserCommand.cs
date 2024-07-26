using MediatR;

namespace Application.Features.GameUser.Commands.AddGameUserCommand
{
    public class AddGameUserCommand : IRequest<AddGameUserCommandResponse>
    {
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
    }
}
