namespace Presentation.Controllers;
public class BasketsController(IServiceManager serviceManager)
    : ApiController
{
    // Get Basket 
    [HttpGet("{key}")]
    public async Task<ActionResult<BasketDto>> GetBasket(string key)
    {
        var basket = await serviceManager.BasketService.GetBasketAsync(key);
        return Ok(basket); 
    }
    // Create Or Update Basket
    [HttpPost]
    public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
    {
        var basketDto = await serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
        return Ok(basketDto);
    }
    // Delete Basket 
    [HttpDelete("{key}")]
    public async Task<ActionResult<bool>> DeleteBasket(string key)
        => await serviceManager.BasketService.DeleteBasketAsync(key);
}