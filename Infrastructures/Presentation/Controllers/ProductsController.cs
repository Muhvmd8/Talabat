namespace Presentation.Controllers;
public class ProductsController(IServiceManager serviceManager)
    : ApiController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ProductResponse>>> GetAllProducts([FromQuery]ProductQueryParameters queryParameters)
    {
        var products = await serviceManager.ProductService.GetAllProductsAsync(queryParameters);
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProduct(int id)
    {
        var product = await serviceManager.ProductService.GetProductAsync(id);
        return Ok(product);
    }
    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<TypeResponse>>> GetTypes()
    {
        var types = await serviceManager.ProductService.GetTypesAsync();
        return Ok(types);
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<BrandResponse>>> GetBrands()
    {
        var brands = await serviceManager.ProductService.GetBrandsAsync();
        return Ok(brands);
    }
}
