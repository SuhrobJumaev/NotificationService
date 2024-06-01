using NotificationService.Interfaces;
using Npgsql;
using System.Data;

namespace NotificationService.Factories
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public NpgsqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
        {
            var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            return conn;
        }
    }
}
