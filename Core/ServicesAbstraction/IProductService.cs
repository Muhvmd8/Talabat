namespace ServicesAbstractions;
public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? brandId, int? typeId);
    Task<ProductResponse?> GetProductAsync(int id);
    Task<IEnumerable<BrandResponse>> GetBrandsAsync();
    Task<IEnumerable<TypeResponse>> GetTypesAsync();
}
