using Dapper;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using NotificationService.Interfaces;
using NotificationService.Models;
using Npgsql;
using System.Data;

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

            string query = @"INSERT INTO event (type, session_id, card, website_url, event_date)
                                        VALUES (@OrderType, @SessionId,@Card,@WebsiteUrl,@EventDate) RETURNING id";

            int eventId = await conn.QueryFirstOrDefaultAsync<int>(new CommandDefinition(query, notifyEventModel, cancellationToken: token));

            return eventId;
        }

        public async Task<IEnumerable<NotificationEventModel>> GetNotificationEventsAsync(CancellationToken token = default)
        {
            using var conn = await _db.CreateConnectionAsync(token);

            string query = @"SELECT id as Id, type as OrderType, session_id as SessionId, website_url as WebsiteUrl, 
                                    event_date as EventDate, created_date as CreatedDate, status as Status, card as Card
                            FROM event
                           WHERE status = 0 AND event_date <= now()";

            IEnumerable<NotificationEventModel> notifications = await conn.QueryAsync<NotificationEventModel>(new CommandDefinition(query, token));

            return notifications;
        }

        public async ValueTask<bool> UpdateNotificationStatusByIdAsync(NotificationEventModel notificationEvent, CancellationToken token = default)
        {
            using var conn = await _db.CreateConnectionAsync(token);

            string query = @"UPDATE event SET status = @Status, sent_date = @SentDate WHERE id = @Id";
            int updatedRow = await conn.ExecuteAsync(new CommandDefinition(query, notificationEvent, cancellationToken: token));

            if(updatedRow > 0)
                return true;
            return false;

        }
    }
}
