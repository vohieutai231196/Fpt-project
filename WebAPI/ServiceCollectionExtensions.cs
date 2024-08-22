using Application.Models;

namespace WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static CacheSettings GetCacheSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var cacheSettingConfigurations = configuration.GetSection("CacheSettings");

            services.Configure<CacheSettings>(cacheSettingConfigurations);

            return cacheSettingConfigurations.Get<CacheSettings>();
        }
    }
}
