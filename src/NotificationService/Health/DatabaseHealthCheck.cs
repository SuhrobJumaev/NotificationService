using Microsoft.Extensions.Diagnostics.HealthChecks;
using NotificationService.Helpers;
using NotificationService.Interfaces;

namespace NotificationService.Health
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<DatabaseHealthCheck> _logger;

        public DatabaseHealthCheck(IDbConnectionFactory dbConnectionFactory, ILogger<DatabaseHealthCheck> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken token = default)
        {
            try
            {
                using var conn = await _dbConnectionFactory.CreateConnectionAsync(token);
                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                _logger.LogError(Utils.HealthCheckErrorMessage, ex);
                return HealthCheckResult.Unhealthy(Utils.HealthCheckErrorMessage, ex);
            }
        }
    }
}
