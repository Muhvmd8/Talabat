using ServicesAbstraction;
namespace Services;
public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper) : IServiceManager
{
    private readonly Lazy<IProductService> _lazyProductService = 
        new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
    public IProductService ProductService => _lazyProductService.Value;
}
