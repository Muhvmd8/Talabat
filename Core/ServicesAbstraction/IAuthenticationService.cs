namespace ServicesAbstraction;
public interface IAuthenticationService
{
    Task<UserDto> LoginAsync(LoginDto loginDto);
    Task<UserDto> RegisterAsync(RegisterDto registerDto);
    Task<bool> CheckEmailAsync(string email);
    Task<UserDto> GetCurrentUserAsync(string email);
    Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto);
    Task<AddressDto> GetCurrentUserAddressAsync(string email);
}
