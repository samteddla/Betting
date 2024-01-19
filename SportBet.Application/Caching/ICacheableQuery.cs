namespace SportBet.Application.Caching;

public interface ICacheableQuery
{
    bool BypassCache { get; }
    string CacheKey { get; }
    TimeSpan? SlidingExpiration { get; }
}