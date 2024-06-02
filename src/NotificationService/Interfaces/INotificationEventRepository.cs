using NotificationService.Dtos;
using NotificationService.Models;

namespace NotificationService.Interfaces
{
    public interface INotificationEventRepository
    {
        ValueTask<int> CreateNotificationEventAsync(NotificationEventModel notifyEventModel, CancellationToken token = default);
        Task<IEnumerable<NotificationEventModel>> GetNotificationEventsAsync(CancellationToken token = default);
        ValueTask<bool> UpdateNotificationStatusByIdAsync(NotificationEventModel notificationEvent, CancellationToken token = default);
    }
}
