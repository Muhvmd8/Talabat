namespace Services;
public static class ApplicationSerivcesResgistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddMaps(typeof(AssemblyReference).Assembly);
        }, typeof(AssemblyReference).Assembly);

        services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

        #region Register Application Services With Its Dependencies Using Factory Delegate
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<Func<IProductService>>(provider =>
            () => provider.GetRequiredService<IProductService>()
        );

        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<Func<IBasketService>>(provider =>
            () => provider.GetRequiredService<IBasketService>()
        );

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<Func<IAuthenticationService>>(provider =>
            () => provider.GetRequiredService<IAuthenticationService>()
        );

        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<Func<IOrderService>>(provider =>
            () => provider.GetRequiredService<IOrderService>()
        );

        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<Func<ICacheService>>(provider =>
            () => provider.GetRequiredService<ICacheService>()
        );
        #endregion

        return services;
    }
}