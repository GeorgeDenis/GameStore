using MediatR;

namespace Application.Features.Users.Queries.GetUserByIdQuery
{
    public class GetUserByIdQuery : IRequest<GetUserByIdQueryResponse>
    {
        public Guid UserId { get; set; }
    }
}
