using Application.Models.Notification;
using Application.Persistence;
using Domain.Common;
using Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;

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

    public async Task<Result<GetNotificationsPaginatedResponseModel>> GetNotificationsByUserIdPaginated(int page, int pageSize, Guid userId)
    {
        var skip = (page - 1) * pageSize;

        var notifications = await context.Notifications
            .Where(x => x.UserId == userId)
            .Skip(skip)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
        var totalItems = await context.Notifications
            .Where(x => x.UserId == userId)
            .CountAsync();

        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var response = new GetNotificationsPaginatedResponseModel
        {
            Notifications = notifications.Select(x => x.Adapt<GetNotificationResponseModel>()).ToList(),
            PageIndex = page,
            TotalPages = totalPages,
            TotalItems = totalItems
        };

        return Result<GetNotificationsPaginatedResponseModel>.Success(response);
    }

}
