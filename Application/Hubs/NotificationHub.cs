
using Application.Persistence;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly INotificationService _notificationService;
        public NotificationHub(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task SendGameNotification(string gameName, string notificationType)
        {
            await _notificationService.CreateNotificationForAllUsers(gameName, notificationType);
            await Clients.All.SendAsync("ReceiveNotification", gameName);
        }
    }
}
