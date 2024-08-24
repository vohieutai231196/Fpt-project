
using Application.Repositories;
using Application.Responsitories;
using Infrastructure.Context;
using Infrastructure.Health;
using Infrastructure.Reponsitories;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services

                    .AddTransient<IPropertyRepon, PropertyRepo>()
                    .AddTransient<IImageRepo, ImageRepo>()
                    .AddDbContext<ApplicationDBContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
                    //.AddHealthChecks()
                    //.AddCheck<DatabaseHealthCheck>("DatabaseCheck")
                    //.AddSqlServer(configuration.GetConnectionString("DefaultConnection"));
                     
        }
    }
}
