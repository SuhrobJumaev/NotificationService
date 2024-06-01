using NotificationService.Dtos;
using NotificationService.Models;
using NotificationService.Models.Enums;

namespace NotificationService.Mappers
{
    public static class NotificationEventMapper
    {
        public static NotificationEventModel DtoToNotificationEventModel(this NotificationEventDto nofifyEventDto)
        {
            return new()
            {
                OrderType = (OrderType)Enum.Parse(typeof(OrderType),nofifyEventDto.OrderType),
                SessionId = nofifyEventDto.SessionId,
                WebsiteUrl = nofifyEventDto.WebsiteUrl,
                EventDate = DateTime.Parse(nofifyEventDto.EventDate),
                Card = nofifyEventDto.Card,
            };
        }
    }
}
