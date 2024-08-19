namespace Application.Models.Notification;

public class GetNotificationResponseModel
{
    public Guid NotificationId { get; set; }
    public Guid UserId { get; set; }
    public string SubjectName { get; set; }
    public string NotificationType { get; set; }
    public DateTime DateSent { get; set; }
    public bool ReadStatus { get; set; }
}
