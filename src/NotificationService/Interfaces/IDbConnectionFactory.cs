using System.Data;

namespace NotificationService.Interfaces
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
    }
}
