using Application.Models.Notification;
using Application.Responses;

namespace Application.Features.Notifications.Queries.GetAllNotificationsByUserIdQuery;

public class GetAllNotificationsByUserIdQueryResponse : BaseResponse
{
    public GetAllNotificationsByUserIdQueryResponse() : base()
    {

    }
    public List<GetNotificationResponseModel> Notifications { get; set; }
}