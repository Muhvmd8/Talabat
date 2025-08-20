namespace Services.MappingProfiles;
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<AddressDto, OrderAddress>().ReverseMap(); 
        
        CreateMap<Order, OrderResponse>()
            .ForMember(d => d.DeliveryMethod, o => o
            .MapFrom(s => s.DeliveryMethod.ShortName));

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());
    }
}
