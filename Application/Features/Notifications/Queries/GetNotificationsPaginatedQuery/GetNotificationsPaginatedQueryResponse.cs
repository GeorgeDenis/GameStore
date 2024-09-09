using Application.Models.Notification;
using Application.Responses;

namespace Application.Features.Notifications.Queries.GetNotificationsPaginatedQuery;

public class GetNotificationsPaginatedQueryResponse : BaseResponse
{
    public GetNotificationsPaginatedQueryResponse() : base()
    {

    }
    public GetNotificationsPaginatedResponseModel Notifications { get; set; }
}