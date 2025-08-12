global using System.Linq.Expressions;
namespace Services.Specifications;
public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
{
    public ProductWithBrandAndTypeSpecifications(int? brandId, int? typeId) 
        : base( p => (!brandId.HasValue || p.BrandId == brandId.Value) &&
        (!typeId.HasValue || p.TypeId == typeId.Value))
    {
        _AddInculdes(p => p.ProductBrand);
        _AddInculdes(p => p.ProductType);
    }
    public ProductWithBrandAndTypeSpecifications(int id)
    : base(p => p.Id == id)
    {
        _AddInculdes(p => p.ProductBrand);
        _AddInculdes(p => p.ProductType);
    }
}
