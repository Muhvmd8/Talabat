global using System.Linq.Expressions;
global using Shared;
namespace Services.Specifications;
public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
{
    public ProductWithBrandAndTypeSpecifications(ProductQueryParameters queryParameters) 
        : base(p => (!queryParameters.BrandId.HasValue || p.BrandId == queryParameters.BrandId.Value) &&
              (!queryParameters.TypeId.HasValue || p.TypeId == queryParameters.TypeId.Value) && 
              (string.IsNullOrWhiteSpace(queryParameters.SearchValue) || p.Name.ToLower()
              .Contains(queryParameters.SearchValue.ToLower())))
    {
        _AddInculdes(p => p.ProductBrand);
        _AddInculdes(p => p.ProductType);

        _AddOrderByOption(queryParameters.Option);

        _ApplyPagination(queryParameters.PageSize, queryParameters.PageIndex);
    }
    public ProductWithBrandAndTypeSpecifications(int id)
       : base(p => p.Id == id)
    {
        _AddInculdes(p => p.ProductBrand);
        _AddInculdes(p => p.ProductType);
    }
    private void _AddOrderByOption(ProductSortingOptions option)
    {
        switch (option)
        {
            case ProductSortingOptions.NameAsc:
                _AddOrderBy(p => p.Name);
                break;
            case ProductSortingOptions.NameDesc:
                _AddOrderByDescending(p => p.Name);
                break;
            case ProductSortingOptions.PriceAsc:
                _AddOrderBy(p => p.Price);
                break;
            case ProductSortingOptions.PriceDesc:
                _AddOrderByDescending(p => p.Price);
                break;
            default:
                break;

        }
    }
}
