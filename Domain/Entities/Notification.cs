using Domain.Common;

namespace Domain.Entities;

public class Notification
{
    public Guid NotificationId { get; set; }
    public Guid UserId { get; set; }
    public string SubjectName { get; set; }
    public string NotificationType { get; set; }
    public DateTime DateSent { get; set; }
    public bool ReadStatus { get; set; }
    public Notification()
    {

    }
    public Notification(Guid userId, string subjectName, string notificationType)
    {
        NotificationId = Guid.NewGuid();
        SubjectName = subjectName;
        NotificationType = notificationType;
        UserId = userId;
        DateSent = DateTime.UtcNow;
        ReadStatus = false;
    }
    public Result<Notification> UpdateReadStatus()
    {
        ReadStatus = true;
        return Result<Notification>.Success(this);
    }
}
