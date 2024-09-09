namespace Application.Models.Notification;

public class GetNotificationsPaginatedResponseModel
{
    public IEnumerable<GetNotificationResponseModel> Notifications { get; set; }
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
}
