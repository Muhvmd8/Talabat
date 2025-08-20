namespace Services.MappingProfiles;
public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<AddressDto, OrderAddress>();   
        CreateMap<Order, OrderResponse>();
    }
}
