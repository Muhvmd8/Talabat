using Persistence.Identity;

namespace Persistence.Repositories;
public class DbInitializer(StoredDbContext _dbContext,
    StoreIdentityDbContext _identityDbContext,
    UserManager<ApplicationUser> _userManager,
    RoleManager<IdentityRole> _roleManager) : IDbInitializer
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
            if (!_dbContext.DeliveryMethods.Any())
            {
                var deliverMethods = File.OpenRead(@"..\Infrastructures\Persistence\Data\Seeding\delivery.json");

                var orderDeliverMethods = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(deliverMethods);
                if (orderDeliverMethods is not null && orderDeliverMethods.Any())
                {
                    await _dbContext.AddRangeAsync(orderDeliverMethods);
                }
            }
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
    public async Task InitializeIdentityAsync()
    {
        try
        {
            if ((await _identityDbContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await _identityDbContext.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            if (!_userManager.Users.Any())
            {
                var user1 = new ApplicationUser
                {
                    Email = "ma7007167@gmail.com",
                    DisplayName = "Mohamed Anwer",
                    PhoneNumber = "01095181541",
                    UserName = "Muhvmd_4"
                };
                var user2 = new ApplicationUser
                {
                    Email = "mo@gmail.com",
                    DisplayName = "Mohamed hany",
                    PhoneNumber = "01234567891",
                    UserName = "Muhvmd_5"
                };

                await _userManager.CreateAsync(user1, "Muhvmd_442004");
                await _userManager.CreateAsync(user2, "Muhvmd_442004");
                await _userManager.AddToRoleAsync(user1, "SuperAdmin");
                await _userManager.AddToRoleAsync(user2, "Admin");
            }

            //await _identityDbContext.SaveChangesAsync(); // Not needed
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
