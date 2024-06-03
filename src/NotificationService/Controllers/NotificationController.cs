using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Dtos;
using NotificationService.Helpers;
using NotificationService.Interfaces;
using NotificationService.Models;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    [ApiVersion(Utils.API_VERSION_1)]
    public class NotificationController : ControllerBase
    {
        private INotificationEventService _notificationService;

        public NotificationController(INotificationEventService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(NotificationEventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationFailureResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNotificationEventAsync([FromBody] NotificationEventDto notifiyEventDto, CancellationToken token = default)
        {
            NotificationEventDto createdNotification = await _notificationService.CreateNotificationEventAsync(notifiyEventDto, token);

            return Ok(createdNotification);
        }
    }
}
