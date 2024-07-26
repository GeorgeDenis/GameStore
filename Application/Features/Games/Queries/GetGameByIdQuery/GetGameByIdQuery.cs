using MediatR;

namespace Application.Features.Games.Queries.GetGameByIdQuery
{
    public class GetGameByIdQuery : IRequest<GetGameByIdQueryResponse>
    {
        public Guid GameId { get; set; }
    }
}
