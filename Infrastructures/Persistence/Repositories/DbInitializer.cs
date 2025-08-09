namespace Persistence.Repositories;
public class DbInitializer(StoredDbContext _dbContext) : IDbInitializer
{
    public async Task InitializeAsync()
    {
		try
		{
            // Migrate any pending migrations and apply them
            if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                await _dbContext.Database.MigrateAsync(); // Apply any pended migrations 

            // Read data for in json and deserialize it.
            // Then add to DbSets
            if (!_dbContext.ProductBrands.Any())
            {
                var brands = File.OpenRead(@"..\Infrastructures\Persistence\Data\Seeding\brands.json");
                // Convert data [ From string => C# objects (ProductBrand) ]
                var productBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(brands);
                if (productBrands is not null && productBrands.Any())
                {
                    await _dbContext.AddRangeAsync(productBrands);
                }
            }
            if (!_dbContext.ProductTypes.Any())
            {
                //var types = File.ReadAllText(@"..\Infrastructures\Persistence\Data\Seeding\types.json");
                var types = File.OpenRead(@"..\Infrastructures\Persistence\Data\Seeding\types.json");

                // Convert data [ From string => C# objects (ProductBrand) ]
                var productTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(types);
                if (productTypes is not null && productTypes.Any())
                {
                    await _dbContext.AddRangeAsync(productTypes);
                }
            }
            if (!_dbContext.Products.Any())
            {
                var products = File.OpenRead(@"..\Infrastructures\Persistence\Data\Seeding\products.json");
                // Convert data [ From string => C# objects (ProductBrand) ]
                var productsObjects = await JsonSerializer.DeserializeAsync<List<Product>>(products);
                if (productsObjects is not null && productsObjects.Any())
                {
                    await _dbContext.AddRangeAsync(productsObjects);
                }
            }
            // Save in database
            await _dbContext.SaveChangesAsync();
        }
		catch (Exception ex)
		{
            Console.WriteLine(ex.Message);   
		}
    }
}
