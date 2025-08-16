using Microsoft.AspNetCore.Builder;

namespace E_Commerce.Web.Extensions;
public static class WebApplicationRegistration
{
    public async static Task InitializeDbAsync(this WebApplication app)
    {
        // Create scope 
        var scope = app.Services.CreateScope();
        var dbInitializer = scope.ServiceProvider
            .GetRequiredService<IDbInitializer>();
        await dbInitializer.InitializeAsync();
    }
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomExceptionHandlerMiddlewares>();
        return app;
    }
    public static IApplicationBuilder UseSwaggerMiddlewares(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
    public static IApplicationBuilder UseApplicationMiddlewares(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
