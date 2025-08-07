namespace Domain.Models.ProductModule;
public class Product : BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    public decimal Price { get; set; }
    public ProductBrand ProductBrand { get; set; }
    public int BrandId { get; set; }
    public ProductType ProductType { get; set; }
    public int TypeId { get; set; }

}
