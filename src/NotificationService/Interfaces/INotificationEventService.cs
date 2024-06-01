using NotificationService.Dtos;

namespace NotificationService.Interfaces
{
    public interface INotificationEventService
    {
        Task<NotificationEventDto> CreateNotificationEventAsync(NotificationEventDto notificationEventDto, CancellationToken token = default);
    }
}
