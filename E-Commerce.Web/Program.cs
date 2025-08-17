namespace E_Commerce.Web;
public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddSwaggerServices();
        builder.Services.AddInfrastructureSerivces(builder.Configuration);
        builder.Services.AddApplicationService();
        builder.Services.AddWebApplicationServices();
        #endregion

        var app = builder.Build(); 

        await app.InitializeDbAsync(); 

        #region Configure the HTTP request pipeline.
        app.UseCustomExceptionMiddleware();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerMiddlewares();
        }
        app.UseApplicationMiddlewares();
        #endregion

        app.Run();
    }

}

