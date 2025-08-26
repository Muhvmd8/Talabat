using System.Text.Json;

namespace Services;
public class CacheService(ICacheRepository cacheRepository) 
    : ICacheService
{
    public async Task<string?> GetAsync(string cacheKey)
    {
        var cacheValue = await cacheRepository.GetAsync(cacheKey);
        return cacheValue is null? null : cacheValue.ToString();
    }
    public async Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive)
    {
        var value = JsonSerializer.Serialize(cacheValue);
        await cacheRepository.SetAsync(cacheKey, value, timeToLive);
    }
}
