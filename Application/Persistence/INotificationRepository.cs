using Application.Models.Notification;
using Domain.Common;
using Domain.Entities;

namespace Application.Persistence;

public interface INotificationRepository : IAsyncRepository<Notification>
{
    Result<IEnumerable<Notification>> GetAllByUserId(Guid userId);
    Task<Result<GetNotificationsPaginatedResponseModel>> GetNotificationsByUserIdPaginated(int page, int pageSize, Guid userId);
}
