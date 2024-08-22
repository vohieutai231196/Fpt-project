using Application.Models;
using Application.PipelineBehaviours.Contracts;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Application.PipelineBehaviours
{
    public class CachePipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheable
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _cacheSettings;

        public CachePipelineBehaviour(IDistributedCache cache, IOptions<CacheSettings> cacheSettings)
        {
            _cache = cache;
            _cacheSettings = cacheSettings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.BypassCache) return await next();

            TResponse response;
            string cacheKey = $"{_cacheSettings.ApplicationName}:{request.CacheKey}";

            var cacheResponse = await _cache.GetAsync(cacheKey, cancellationToken);

            if (cacheResponse != null)
            {
                response = JsonConvert.DeserializeObject<TResponse>(Encoding.Default.GetString(cacheResponse));
            }
            else
            {
                // Get the response and write to cache
                response = await GetResponseAndWriteToCacheAsync();

            }
            
            return response;

            async Task<TResponse> GetResponseAndWriteToCacheAsync()
            {
                response = await next();

                if (response != null)
                {
                    var slidingExpiration = request.SlidingExpiration == null ? 
                        TimeSpan.FromMinutes(_cacheSettings.SlidingExpiration) 
                        : request.SlidingExpiration;

                    var cacheOptions = new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = slidingExpiration,
                        AbsoluteExpiration = DateTime.Now.AddDays(1)
                    };

                    var serializedData = Encoding.Default
                        .GetBytes(
                            JsonConvert
                            .SerializeObject(response,
                                Formatting.Indented,
                                new JsonSerializerSettings()
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                }));

                    await _cache.SetAsync(cacheKey, serializedData, cacheOptions, cancellationToken);
                }

                return response;
            }
        }
    }
}
