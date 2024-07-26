using Application.Models.User;
using Application.Persistence;
using Mapster;
using MediatR;

namespace Application.Features.Users.Queries.GetUserByIdQuery
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
    {
        private readonly IUserRepository userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindByIdAsync(request.UserId);
            if (!user.IsSuccess)
            {
                return new GetUserByIdQueryResponse
                {
                    Success = false,
                    Message = "User with this id doesn't exist!"
                };
            }
            return new GetUserByIdQueryResponse
            {
                Success = true,
                User = user.Value.Adapt<GetUserModel>()
            };
        }
    }
}
