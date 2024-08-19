using MediatR;

namespace Application.Features.GameUser.Queries.GetPurchaseStatus
{
    public class GetPurchaseStatusQuery : IRequest<GetPurchaseStatusQueryResponse>
    {
        public Guid GameId { get; set; }

    }
}
