using HealthChecks.SqlServer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Primitives;
using System.Data;

namespace Infrastructure.Health
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly SqlServerHealthCheckOptions _options;

        public DatabaseHealthCheck(SqlServerHealthCheckOptions options, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            options.ConnectionString = connectionString;
            //Guard.ThrowIfNull(options.ConnectionString, connectionString);
            //Guard.ThrowIfNull(options.CommandText, "true");
            _options = options;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var connection = new SqlConnection(_options.ConnectionString);

                _options.Configure?.Invoke(connection);
                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                using var command = connection.CreateCommand();
                command.CommandText = _options.CommandText;
                object result = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);

                return _options.HealthCheckResultBuilder == null
                    ? HealthCheckResult.Healthy()
                    : _options.HealthCheckResultBuilder(result);
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
            }
        }
    }
}
