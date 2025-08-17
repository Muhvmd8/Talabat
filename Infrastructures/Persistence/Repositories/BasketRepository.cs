namespace Persistence.Repositories;
public class BasketRepository(IConnectionMultiplexer connection)
    : IBasketRepository
{
    private readonly IDatabase _database = connection.GetDatabase();
    public async Task<CustomerBasket?> GetBasketAsync(string id)
    {
        var basket = await _database.StringGetAsync(id);
        if (basket.IsNullOrEmpty) return null;

        return JsonSerializer.Deserialize<CustomerBasket>(basket!);
    }
    public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
    {
        var jsonBasket = JsonSerializer.Serialize(basket);
        var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, timeToLive?? TimeSpan.FromDays(30));
        if (isCreatedOrUpdated) return await GetBasketAsync(basket.Id);
        else return null;
    }
    public async Task<bool> DeleteBasketAsync(string id) => await _database.KeyDeleteAsync(id);
}
