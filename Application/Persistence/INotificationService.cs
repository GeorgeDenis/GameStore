using Domain.Entities;

namespace Application.Persistence;

public interface INotificationService
{
    Task<IEnumerable<Notification>> CreateNotificationForAllUsers(string subjectName, string notificationType);
    Task DeleteNotificationsOlderThanADay();

}
