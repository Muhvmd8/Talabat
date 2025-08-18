namespace Services;
public class ServiceManager(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IMapper mapper, UserManager<ApplicationUser> userManager) : IServiceManager
{
    private readonly Lazy<IProductService> _lazyProductService = 
        new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
    public IProductService ProductService => _lazyProductService.Value;

    private readonly Lazy<IBasketService> _lazyBasketService =
        new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
    public IBasketService BasketService => _lazyBasketService.Value;

    private readonly Lazy<IAuthenticationService> _lazyAuthenticationService =
        new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager));
    public IAuthenticationService AuthenticationService => _lazyAuthenticationService.Value;
}
