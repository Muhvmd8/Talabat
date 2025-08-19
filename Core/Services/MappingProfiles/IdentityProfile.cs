namespace Services.MappingProfiles;
public class IdentityProfile : Profile
{
    public IdentityProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}
