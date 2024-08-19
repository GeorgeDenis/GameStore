using Application.Features.GameUser.Queries.GetGamesByUserIdQuery;
using Application.Models.Notification;
using Application.Models.User;
using Application.Persistence;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Notifications.Queries.GetAllNotificationsByUserIdQuery;

public class GetAllNotificationsByUserIdQueryHandler : IRequestHandler<GetAllNotificationsByUserIdQuery, GetAllNotificationsByUserIdQueryResponse>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ITokenService _tokenService;
    public GetAllNotificationsByUserIdQueryHandler(INotificationRepository notificationRepository, ITokenService tokenService)
    {
        _notificationRepository = notificationRepository;
        _tokenService = tokenService;
    }
    public async Task<GetAllNotificationsByUserIdQueryResponse> Handle(GetAllNotificationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _tokenService.GetUserId();
        if (!userId.IsSuccess)
        {
            return new GetAllNotificationsByUserIdQueryResponse
            {
                Success = false,
                Message = userId.Value,
            };
        }
        var notifications = _notificationRepository.GetAllByUserId(Guid.Parse(userId.Value));
        if (!notifications.IsSuccess)
        {
            return new GetAllNotificationsByUserIdQueryResponse
            {
                Success = false,
                Message = "Something went wrong when trying to get notifications"
            };
        }
        return new GetAllNotificationsByUserIdQueryResponse
        {
            Success = true,
            Notifications = notifications.Value.Select(x => x.Adapt<GetNotificationResponseModel>()).ToList(),
        };
    }
}
