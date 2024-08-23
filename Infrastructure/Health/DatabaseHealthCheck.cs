using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;

namespace Infrastructure.Health
{
    internal sealed class DatabaseHealthCheck(ApplicationDBContext dbConnectionFactory) : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, 
            CancellationToken cancellationToken = default)
        {
            //using IDbConnection connection = dbConnectionFactory.Database.OpenConnection();
            throw new NotImplementedException();
        }
    }
}
