namespace ServicesAbstraction;
public interface ICacheService
{
    public Task<string?> GetAsync(string cacheKey);
    public Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive);
}