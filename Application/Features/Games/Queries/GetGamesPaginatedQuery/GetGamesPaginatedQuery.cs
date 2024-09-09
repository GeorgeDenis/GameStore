using MediatR;

namespace Application.Features.Games.Queries.GetGamesPaginatedQuery;

public class GetGamesPaginatedQuery : IRequest<GetGamesPaginatedQueryResponse>
{
    public int Page { get; set; }
    public int Size { get; set; }
}
