using Shared;

namespace ServicesAbstractions;
public interface IProductService
{
    Task<PaginatedResult<ProductResponse>> GetAllProductsAsync(ProductQueryParameters queryParameters);
    Task<ProductResponse?> GetProductAsync(int id);
    Task<IEnumerable<BrandResponse>> GetBrandsAsync();
    Task<IEnumerable<TypeResponse>> GetTypesAsync();
}
