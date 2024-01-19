using ErrorOr;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using SportBet.Application.Caching;
using System.Text;
using System.Text.Json;


namespace SportBet.Application.Behaviours;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
 where TRequest : ICacheableQuery
 where TResponse : IErrorOr
{
    private readonly IDistributedCache _cache;
    private readonly CacheSettings _settings;

    public CachingBehavior(IDistributedCache cache, IOptions<CacheSettings> settings)
    {
        _cache = cache;
        _settings = settings.Value;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse response;

        if (request.BypassCache)
        {
            return await next();
        }

        async Task<TResponse> GetResponseAndAddToCache()
        {
            response = await next();

            var options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = request.SlidingExpiration == null ? TimeSpan.FromMinutes(_settings.SlidingExpiration) : request.SlidingExpiration
            };
            var serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(response));
            await _cache.SetAsync(request.CacheKey, serializedData, options, cancellationToken);

            return response;
        }

        var cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
        if (cachedResponse != null)
        {
            using var stream = new MemoryStream(cachedResponse);
            return await JsonSerializer.DeserializeAsync<TResponse>(stream, cancellationToken: cancellationToken) ?? await GetResponseAndAddToCache();
        }
        else
        {
            response = await GetResponseAndAddToCache();
        }

        return response;
    }
}
