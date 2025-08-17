namespace Services;
public class BasketService(IBasketRepository basketRepository, IMapper mapper) 
    : IBasketService
{
    public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
    {
        var customerBasket = mapper.Map<BasketDto, CustomerBasket>(basket);  
        var CreatedOrUpdatedBasket = await basketRepository.CreateOrUpdateBasketAsync(customerBasket);
        if (CreatedOrUpdatedBasket is not null)
            return await GetBasketAsync(basket.Id);
        else
            throw new Exception("Can not update or create basket now, try again later!");
    }
    public async Task<bool> DeleteBasketAsync(string key) 
        => await basketRepository.DeleteBasketAsync(key);
    public async Task<BasketDto> GetBasketAsync(string key)
    {
        var basket = await basketRepository.GetBasketAsync(key);
        if (basket is null) throw new BasketNotFoundException(key);

        return mapper.Map<CustomerBasket, BasketDto>(basket);
    }
}
