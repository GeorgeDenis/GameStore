using Application.Persistence;
using Domain.Common;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
{
    public NotificationRepository(SteamContext context) : base(context)
    {

    }

    public Result<IEnumerable<Notification>> GetAllByUserId(Guid userId)
    {
        var notifications = context.Notifications.Where(x => x.UserId == userId).ToList();
        return Result<IEnumerable<Notification>>.Success(notifications);
    }
}
