namespace E_Commerce.Web.Extensions;
public static class WebApplicationRegistration
{
    public async static Task InitializeDbAsync(this WebApplication app)
    {
        // Create scope 
        var scope = app.Services.CreateScope();
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        await dbInitializer.InitializeAsync();
        await dbInitializer.InitializeIdentityAsync();
    }
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomExceptionHandlerMiddlewares>();
        return app;
    }
    public static IApplicationBuilder UseSwaggerMiddlewares(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Talabat v1");
            options.DisplayRequestDuration();
            options.DocumentTitle = "Talabat";
            options.DocExpansion(DocExpansion.None);
            options.JsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            options.EnableFilter();
            options.EnablePersistAuthorization();
        });
        return app;
    }
    public static IApplicationBuilder UseApplicationMiddlewares(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
