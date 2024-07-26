using Application.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Features.Developers.Commands.CreateDeveloperCommand
{
    public class CreateDeveloperCommandHandler : IRequestHandler<CreateDeveloperCommand, CreateDeveloperCommandResponse>
    {
        private readonly IDeveloperRepository developerRepository;
        public CreateDeveloperCommandHandler(IDeveloperRepository developerRepository)
        {
            this.developerRepository = developerRepository;
        }

        public async Task<CreateDeveloperCommandResponse> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
        {
            var developer = new Developer(request.Name, request.Description);
            var result = await developerRepository.AddAsync(developer);
            if (!result.IsSuccess)
            {
                return new CreateDeveloperCommandResponse
                {
                    Success = false,
                    Message = "Something went wrong while creating a developer"
                };
            }
            return new CreateDeveloperCommandResponse
            {
                Success = true,
            };
        }
    }
}
