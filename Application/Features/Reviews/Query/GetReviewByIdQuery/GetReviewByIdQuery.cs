using MediatR;

namespace Application.Features.Reviews.Query.GetReviewByIdQuery
{
    public class GetReviewByIdQuery : IRequest<GetReviewByIdQueryResponse>
    {
        public Guid ReviewId { get; set; }
    }
}
