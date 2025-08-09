global using Microsoft.AspNetCore.Mvc;
global using ServicesAbstraction;
global using Shared.DataTransferObjects.ProductDTO;
namespace Presentation.Controllers;
[ApiController]
[Route("api/[Controller]")] 
public class ProductsController(IServiceManager serviceManager)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts()
    {
        var products = await serviceManager.ProductService.GetAllProductsAsync();
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
