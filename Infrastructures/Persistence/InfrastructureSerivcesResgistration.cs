using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

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
        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
