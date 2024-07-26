using MediatR;

namespace Application.Features.Reviews.Query.GetReviewsByGameIdQuery
{
    public class GetReviewsByGameIdQuery : IRequest<GetReviewsByGameIdQueryResponse>
    {
        public Guid GameId { get; set; }
    }
}
