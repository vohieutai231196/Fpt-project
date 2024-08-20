using Application.Responsitories;
using Infrastructure.Context;
using Infrastructure.Reponsitories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
           return services
                .AddTransient<IPropertyRepon, PropertyRepo>()
                .AddDbContext<ApplicationDBContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
