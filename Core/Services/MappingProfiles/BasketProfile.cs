namespace Services.MappingProfiles;
public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<BasketDto, CustomerBasket>().ReverseMap();
        CreateMap<BasketItem, BasketItemResponse>().ReverseMap();
    }
}
