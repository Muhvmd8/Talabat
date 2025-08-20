using Order = Domain.Models.OrderModule.Order;
namespace Presentation.Data.Context;
public class StoredDbContext(DbContextOptions<StoredDbContext> options) 
    : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ProductBrand> ProductBrands { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly); // Open-Closed Principle
    }
}