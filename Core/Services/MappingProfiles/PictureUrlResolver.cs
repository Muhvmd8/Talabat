global using Microsoft.Extensions.Configuration;
internal class PictureUrlResolver(IConfiguration configuration)
    : IValueResolver<Product, ProductResponse, string>
{
    public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        => (string.IsNullOrEmpty(source.PictureUrl))? string.Empty 
        : $"{configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
}