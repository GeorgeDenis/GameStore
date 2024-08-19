using Application.Features.Notifications.Commands.ReadNotificationCommand;
using Application.Features.Notifications.Queries.GetAllNotificationsByUserIdQuery;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NotificationController : ApiControllerBase
    {
        [HttpGet("userId")]
        public async Task<IActionResult> GetAll()
        {
            var response = await Mediator.Send(new GetAllNotificationsByUserIdQuery());
            return Ok(response);
        }
        [HttpGet("readStatus/{notificationId}")]
        public async Task<IActionResult> UpdateNotificationReadStatus(Guid notificationId)
        {
            var command = new ReadNotificationCommand { NotificationId = notificationId };
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
