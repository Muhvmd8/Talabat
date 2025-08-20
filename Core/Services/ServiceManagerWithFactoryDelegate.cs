
namespace Services;
public class ServiceManagerWithFactoryDelegate(Func<IProductService> _productFactory,
    Func<IBasketService> _basketFactory,
    Func<IAuthenticationService> _authenticationFactory,
    Func<IOrderService> _orderFactory)
    : IServiceManager
{
    public IProductService ProductService => _productFactory();
    public IBasketService BasketService => _basketFactory();
    public IAuthenticationService AuthenticationService => _authenticationFactory();
    public IOrderService OrderService => _orderFactory();
}
