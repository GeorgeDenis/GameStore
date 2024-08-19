using Domain.Common;
using Domain.Entities;

namespace Application.Persistence;

public interface INotificationRepository : IAsyncRepository<Notification>
{
    Result<IEnumerable<Notification>> GetAllByUserId(Guid userId);
}
