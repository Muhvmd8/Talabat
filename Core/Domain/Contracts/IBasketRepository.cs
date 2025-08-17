namespace Domain.Contracts;
public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(string id);    
    Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = default);
    Task<bool> DeleteBasketAsync(string id);
}
