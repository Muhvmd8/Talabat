namespace Services.Specifications;
internal class ProductCountSpecification : BaseSpecifications<Product, int>
{
    public ProductCountSpecification(ProductQueryParameters queryParameters)
        : base(p => (!queryParameters.BrandId.HasValue || p.BrandId == queryParameters.BrandId.Value) &&
              (!queryParameters.TypeId.HasValue || p.TypeId == queryParameters.TypeId.Value) &&
              (string.IsNullOrWhiteSpace(queryParameters.SearchValue) || p.Name.ToLower()
              .Contains(queryParameters.SearchValue.ToLower())))
    { }
}
