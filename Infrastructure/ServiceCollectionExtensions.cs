
using Application.Repositories;
using Application.Responsitories;
using Infrastructure.Context;
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
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                     .AddTransient<IPropertyRepon, PropertyRepo>()
                     .AddTransient<IImageRepo, ImageRepo>()
                     .AddDbContext<ApplicationDBContext>(options => options
                     .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
