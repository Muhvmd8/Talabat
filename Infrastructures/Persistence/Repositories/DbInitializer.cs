namespace Persistence.Repositories;
public class DbInitializer(StoredDbContext _dbContext) : IDbInitializer
{
    public void Initialize()
    {
		try
		{
            // Migrate any pending migrations and apply them
            if (_dbContext.Database.GetPendingMigrations().Any())
                _dbContext.Database.Migrate(); // Apply any pended migrations 

            // Read data for in json and deserialize it.
            // Then add to DbSets
            if (!_dbContext.ProductBrands.Any())
            {
                var brands = File.ReadAllText(@"..\Infrastructures\Persistence\Data\Seeding\brands.json");
                // Convert data [ From string => C# objects (ProductBrand) ]
                var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(brands);
                if (productBrands is not null && productBrands.Any())
                {
                    _dbContext.AddRange(productBrands);
                }
            }
            if (!_dbContext.ProductTypes.Any())
            {
                var types = File.ReadAllText(@"..\Infrastructures\Persistence\Data\Seeding\types.json");
                // Convert data [ From string => C# objects (ProductBrand) ]
                var productTypes = JsonSerializer.Deserialize<List<ProductType>>(types);
                if (productTypes is not null && productTypes.Any())
                {
                    _dbContext.AddRange(productTypes);
                }
            }
            if (!_dbContext.Products.Any())
            {
                var products = File.ReadAllText(@"..\Infrastructures\Persistence\Data\Seeding\products.json");
                // Convert data [ From string => C# objects (ProductBrand) ]
                var productsObjects = JsonSerializer.Deserialize<List<Product>>(products);
                if (productsObjects is not null && productsObjects.Any())
                {
                    _dbContext.AddRange(productsObjects);
                }
            }
            // Save in database
            _dbContext.SaveChanges();
        }
		catch (Exception ex)
		{
            Console.WriteLine(ex.Message);   
		}
    }
}
