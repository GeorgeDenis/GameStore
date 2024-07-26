using MediatR;

namespace Application.Features.Developers.Queries.GetDeveloperByIdQuery
{
    public class GetDeveloperByIdQuery : IRequest<GetDeveloperByIdQueryResponse>
    {
        public Guid DeveloperId { get; set; }
    }
}
