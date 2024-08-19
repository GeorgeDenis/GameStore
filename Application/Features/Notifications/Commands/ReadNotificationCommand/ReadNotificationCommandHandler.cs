using Application.Persistence;
using MediatR;

namespace Application.Features.Notifications.Commands.ReadNotificationCommand;

public class ReadNotificationCommandHandler : IRequestHandler<ReadNotificationCommand, ReadNotificationCommandResponse>
{
    private readonly INotificationRepository _notificationRepository;
    public ReadNotificationCommandHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }
    public async Task<ReadNotificationCommandResponse> Handle(ReadNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _notificationRepository.FindByIdAsync(request.NotificationId);
        if (!notification.IsSuccess)
        {
            return new ReadNotificationCommandResponse
            {
                Success = false,
                Message = "Notification with this id doesn't exist"
            };
        }
        notification.Value.ReadStatus = true;
        var notificationUpdate = notification.Value.UpdateReadStatus();
        if (!notificationUpdate.IsSuccess)
        {
            return new ReadNotificationCommandResponse
            {
                Success = false,
                Message = "Error while trying to update read status"
            };
        }
        var response = await _notificationRepository.UpdateAsync(notificationUpdate.Value);
        if (!response.IsSuccess)
        {
            return new ReadNotificationCommandResponse
            {
                Success = false,
                Message = "Updating this notification has failed"
            };
        }
        return new ReadNotificationCommandResponse
        {
            Success = true,
        };
    }
}
