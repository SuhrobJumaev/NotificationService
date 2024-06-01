using NotificationService.Models.Enums;

namespace NotificationService.Models
{
    public class NotificationEventModel
    {
        public int Id { get; init; }
        public OrderType OrderType { get; init; }
        public string SessionId { get; init; }
        public string Card { get; init; }
        public DateTime EventDate { get; init; }
        public string WebsiteUrl { get; init; }
        public NotificationStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime SentDate { get; set; }

    }
}
