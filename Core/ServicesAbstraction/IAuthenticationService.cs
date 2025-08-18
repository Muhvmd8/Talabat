namespace ServicesAbstraction;
public interface IAuthenticationService
{
    Task<UserDto> LoginAsync(LoginDto loginDto);
    Task<UserDto> RegisterAsync(RegisterDto registerDto);
}
