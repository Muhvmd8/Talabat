using Domain.Models.ProductModule;

namespace E_Commerce.Web;
public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<StoredDbContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });
        builder.Services.AddScoped<IDbInitializer, DbInitializer>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddAutoMapper(config =>
        {
            config.AddMaps(typeof(AssemblyReference).Assembly);
        },
        typeof(AssemblyReference).Assembly);
        builder.Services.AddScoped<IServiceManager, ServiceManager>();
        #endregion

        IEnumerable<ProductBrand> productBrands;
        productBrands = [];
        productBrands.Where(p => p.Id == 10).Select(p => p.Name);

        var app = builder.Build(); 

        await app.InitializeDbAsync(); 

        #region Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseAuthorization();

        app.MapControllers(); 
        #endregion

        app.Run();


    }
}

