using MediatR;

namespace Application.Features.Notifications.Commands.ReadNotificationCommand;

public class ReadNotificationCommand : IRequest<ReadNotificationCommandResponse>
{
    public Guid NotificationId { get; set; }
}
