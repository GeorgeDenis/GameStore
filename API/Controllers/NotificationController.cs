using Application.Features.Notifications.Commands.ReadNotificationCommand;
using Application.Features.Notifications.Queries.GetAllNotificationsByUserIdQuery;
using Application.Features.Notifications.Queries.GetNotificationsPaginatedQuery;
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
        [HttpGet("paginated")]
        public async Task<IActionResult> GetNotificationsPaginated(int page, int pageSize)
        {
            var query = new GetNotificationsPaginatedQuery
            {
                Page = page,
                Size = pageSize
            };
            var response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
