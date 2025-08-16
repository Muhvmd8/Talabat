namespace Services;
public static class ApplicationSerivcesResgistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddMaps(typeof(AssemblyReference).Assembly);
        }, typeof(AssemblyReference).Assembly);
        services.AddScoped<IServiceManager, ServiceManager>();
        return services;
    }
}
