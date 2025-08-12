global using System.Linq.Expressions;
global using Shared;
namespace Services.Specifications;
public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
{
    public ProductWithBrandAndTypeSpecifications(int? brandId, int? typeId, ProductSortingOptions option) 
        : base( p => (!brandId.HasValue || p.BrandId == brandId.Value) &&
        (!typeId.HasValue || p.TypeId == typeId.Value))
    {
        _AddInculdes(p => p.ProductBrand);
        _AddInculdes(p => p.ProductType);

        _AddOrderByOption(option);
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
