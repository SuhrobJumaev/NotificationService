using Dapper;
using NotificationService.Interfaces;
using NotificationService.Models;

namespace NotificationService.Repositories
{
    public class NotificationEventRepository : INotificationEventRepository
    {
        private IDbConnectionFactory _db;

        public NotificationEventRepository(IDbConnectionFactory db)
        {
            _db = db;
        }

        public async ValueTask<int> CreateNotificationEventAsync(NotificationEventModel notifyEventModel, CancellationToken token = default)
        {
            using var conn = await _db.CreateConnectionAsync(token);

            string query = @"INSERT INTO event (type, session_id, card, website_url event_date, created_date, sent_date)
                                        VALUES (@Type, @SessionId,@Card,@WebsiteUrl,@EventDate, @CreatedDate, @SentDate) RETURNING id";

            int eventId = await conn.QueryFirstOrDefaultAsync<int>(new CommandDefinition(query, notifyEventModel, cancellationToken: token));

            return eventId;
        }
    }
}
