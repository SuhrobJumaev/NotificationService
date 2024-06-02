using Dapper;
using NotificationService.Interfaces;

namespace NotificationService.Helpers
{
    public class DbInitializer
    {
        private readonly IDbConnectionFactory _db;

        public DbInitializer(IDbConnectionFactory db)
        {
            _db = db;
        }

        public async Task InitializeAsync()
        {
            using var conn = await _db.CreateConnectionAsync();

            await conn.ExecuteAsync("""
                DO $$ 
            BEGIN
                IF NOT EXISTS (SELECT FROM pg_database WHERE datname = 'notification_service') THEN
                    CREATE DATABASE notification_service
                        WITH
                        OWNER = postgres
                        ENCODING = 'UTF8'
                        LC_COLLATE = 'ru_RU.UTF-8'
                        LC_CTYPE = 'ru_RU.UTF-8'
                        CONNECTION LIMIT = -1;
                END IF;
            END $$;
            
            """);



            await conn.ExecuteAsync("""
            CREATE TABLE IF NOT EXISTS "event" (
                id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                type SMALLINT NOT NULL,
                session_id VARCHAR(30) NOT NULL,
                card VARCHAR(20) NOT NULL,
                event_date TIMESTAMP NOT NULL,
                website_url VARCHAR(50) NOT NULL,
                created_date TIMESTAMP DEFAULT now(),
                sent_date TIMESTAMP NULL,
                status SMALLINT DEFAULT 0)
         """);

            await conn.ExecuteAsync("""
            CREATE INDEX IF NOT EXISTS status_created_date
            ON "event" (status, event_date);
            """);
        }
    }
}
