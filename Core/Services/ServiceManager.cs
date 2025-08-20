namespace Services;
public class ServiceManager(IUnitOfWork unitOfWork,
    IBasketRepository basketRepository,
    IMapper mapper, 
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration)

    : IServiceManager
{
    private readonly Lazy<IProductService> _lazyProductService = 
        new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
    private readonly Lazy<IBasketService> _lazyBasketService =
        new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
    private readonly Lazy<IAuthenticationService> _lazyAuthenticationService =
        new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, configuration, mapper));
    private readonly Lazy<IOrderService> _lazyOrderService =
        new Lazy<IOrderService>(() => new OrderService(mapper, unitOfWork, basketRepository));

    public IProductService ProductService => _lazyProductService.Value;
    public IBasketService BasketService => _lazyBasketService.Value;
    public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
    public IOrderService OrderService => _lazyOrderService.Value;
}
