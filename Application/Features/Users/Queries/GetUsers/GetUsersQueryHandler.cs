using Application.Models.User;
using Application.Persistence;
using Mapster;
using MediatR;

namespace Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersQueryResponse>
    {
        private readonly IUserRepository userRepository;
        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<GetUsersQueryResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAllAsync();
            return new GetUsersQueryResponse
            {
                Success = true,
                Users = users.Value.Select(x => x.Adapt<GetUserModel>()).ToList(),
            };
        }
    }
}
