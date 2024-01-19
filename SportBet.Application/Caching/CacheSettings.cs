namespace SportBet.Application.Caching;

public class CacheSettings
{
    public const string SectionName = "CacheSettings";
    public int SlidingExpiration { get; set; }
}