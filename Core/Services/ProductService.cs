global using Services.Specifications;
using Shared;
namespace Services;
public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync(ProductQueryParameters queryParameters)
    {
        var specifications = new ProductWithBrandAndTypeSpecifications(queryParameters);
        var products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
        // Map from 'Product' to 'ProductResponse'
        var productsResponse = mapper.Map< IEnumerable <Product>, IEnumerable<ProductResponse>>(products);
        return productsResponse;
    }
    public async Task<ProductResponse?> GetProductAsync(int id)
    {
        var specifications = new ProductWithBrandAndTypeSpecifications(id);
        var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
        if (product == null) return null;
        return mapper.Map<Product, ProductResponse>(product);
    }
    public async Task<ProductResponse?> GetProductAsync(ISpecifications<Product, int> specifications)
    {
        var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(specifications);
        if (product == null) return null;
        return mapper.Map<Product, ProductResponse>(product);
    }
    public async Task<IEnumerable<BrandResponse>> GetBrandsAsync()
    {
        var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
        var brandsResponse = mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandResponse>>(brands);
        return brandsResponse;
    }
    public async Task<IEnumerable<TypeResponse>> GetTypesAsync()
    {
        var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
        var typesResponse = mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeResponse>>(types);
        return typesResponse;
    }
}