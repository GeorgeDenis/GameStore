using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.CreateUserCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IUserRepository userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name, request.Email, request.Password);
            var result = await userRepository.AddAsync(user);
            if (!result.IsSuccess)
            {
                return new CreateUserCommandResponse
                {
                    Success = false,
                    Message = "Something went wrong while creating the user"
                };
            }
            return new CreateUserCommandResponse
            {
                Success = true,
            };
        }
    }
}
