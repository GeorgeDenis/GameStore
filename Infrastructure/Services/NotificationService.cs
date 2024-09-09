using Application.Persistence;
using Domain.Entities;

namespace Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRepository _userRepository;
    public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository)
    {
        _notificationRepository = notificationRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Notification>> CreateNotificationForAllUsers(string subjectName, string notificationType)
    {
        var userIds = await _userRepository.GetAllUserIds();
        var notificationList = new List<Notification>();
        foreach (var userId in userIds)
        {
            var notification = new Notification(userId, subjectName, notificationType);
            var result = await _notificationRepository.AddAsync(notification);
            notificationList.Add(notification);
        }
        return notificationList;
    }

    public async Task DeleteNotificationsOlderThanADay()
    {
        var oneDayAgo = DateTime.UtcNow.AddDays(-1);
        var notifications = await _notificationRepository.GetAllAsync();

        var notificationsToDelete = notifications.Value
            .Where(x => x.ReadStatus && x.DateSent <= oneDayAgo)
            .ToList();

        if (notificationsToDelete.Count != 0)
        {
            foreach (var notification in notificationsToDelete)
            {
                await _notificationRepository.DeleteAsync(notification.NotificationId);
            }
        }
    }

}
