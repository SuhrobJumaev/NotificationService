namespace NotificationService.Dtos
{
    public readonly struct NotificationEventDto
    {
        public int Id { get; init; }
        public string OrderType { get; init; }
        public string SessionId { get; init; }
        public string Card { get; init; }
        public string EventDate { get; init; }
        public string WebsiteUrl { get; init; }
    }
}
