using Microsoft.AspNetCore.Mvc;
using NotificationService.Dtos;
using NotificationService.Interfaces;
using NotificationService.Models;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private INotificationEventService _notificationService;

        public NotificationController(INotificationEventService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(NotificationEventDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNotificationEventAsync([FromBody] NotificationEventDto notifiyEventDto, CancellationToken token = default)
        {
            NotificationEventDto createdNotification = await _notificationService.CreateNotificationEventAsync(notifiyEventDto, token);

            return CreatedAtAction("GetNotificatonEvent", new { id = createdNotification.Id }, createdNotification);
        }
    }
}
