namespace Persistence;
public static class InfrastructureSerivcesResgistration
{
    public static IServiceCollection AddInfrastructureSerivces(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoredDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });
        services.AddDbContext<StoreIdentityDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("IdentityConnection");
            options.UseSqlServer(connectionString);
        });
        services.AddSingleton<IConnectionMultiplexer>((_) =>
        {
            var connectionString = configuration.GetConnectionString("RedisConnection");
            return ConnectionMultiplexer.Connect(connectionString!);
        });

        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.User.RequireUniqueEmail = true; // <--- this enforces unique emails
        })
          .AddRoles<IdentityRole>()
          .AddEntityFrameworkStores<StoreIdentityDbContext>();
        return services;
    }
}
