namespace Domain.Contracts;
public interface ICacheRepository
{
    public Task<string?> GetAsync(string cacheKey);
    public Task SetAsync(string cacheKey, string cacheValue, TimeSpan timeToLive);
}
