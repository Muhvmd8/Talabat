namespace Services.MappingProfiles;
public class OrderItemPictureUrlResolver(IConfiguration configuration) 
    : IValueResolver<OrderItem, OrderItemDto, string>
{
    private readonly IConfiguration _configuration = configuration;

    public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.Product.PictureUrl))
            return string.Empty;

        return $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
    }
}
