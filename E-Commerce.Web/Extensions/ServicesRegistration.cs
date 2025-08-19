using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace E_Commerce.Web.Extensions;
public static class ServicesRegistration
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
    public static IServiceCollection AddWebApplicationServices(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorsResponse;
        });

        return services;
    }
    public static IServiceCollection AddJWTService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            //config.SaveToken = true; // Save the JWT in the http context 
            config.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["JWTOptions:Issuer"],

                ValidateAudience = true,
                ValidAudience = configuration["JWTOptions:Audience"],

                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTOptions:SecretKey"]!))
            };
        });

        return services;
    }
}
