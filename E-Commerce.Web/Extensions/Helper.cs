namespace E_Commerce.Web.Extensions;
public static class Helper
{
    public async static Task InitializeDbAsync(this WebApplication app)
    {
        // Create scope 
        var scope = app.Services.CreateScope();
        var dbInitializer = scope.ServiceProvider
            .GetRequiredService<IDbInitializer>();
        await dbInitializer.InitializeAsync();
    }
}
