using MediatR;

namespace Application.Features.Notifications.Queries.GetNotificationsPaginatedQuery;

public class GetNotificationsPaginatedQuery : IRequest<GetNotificationsPaginatedQueryResponse>
{
    public int Page { get; set; }
    public int Size { get; set; }
}
