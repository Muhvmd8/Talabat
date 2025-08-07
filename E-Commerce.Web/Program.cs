namespace E_Commerce.Web;
public class Program
{
    public static void Main(string[] args)
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
        #endregion

        var app = builder.Build();
        InitializeDb(app);

        #region Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers(); 
        #endregion

        app.Run();
    }
    public static void InitializeDb(WebApplication app)
    {
        // Create scope 
        var scope = app.Services.CreateScope();
        var dbInitializer = scope.ServiceProvider
            .GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    } 
}

