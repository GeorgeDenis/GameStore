using Application.Features.Developers.Queries.GetDevelopers;
using MediatR;

namespace Application.Features.Developers.Queries.GetDevelopersQuery
{
    public class GetDevelopersQuery : IRequest<GetDevelopersQueryResponse>
    {
    }
}
