global using Shared.DataTransferObjects.BasketDTO;
namespace ServicesAbstraction;
public interface IBasketService
{
    Task<BasketDto> GetBasketAsync(string key);
    Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket);
    Task<bool> DeleteBasketAsync(string key);   
}
