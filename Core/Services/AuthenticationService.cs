namespace Services;
public class AuthenticationService(UserManager<ApplicationUser> userManager)
    : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    public async Task<UserDto> LoginAsync(LoginDto loginDto)
    {
        // Check if email is exists
        var user = await _userManager.FindByEmailAsync(loginDto.Email) ?? throw new UserNotFoundException(loginDto.Email);
        // Check password
        var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        // return UserDto
        if (isValidPassword)
            return new UserDto
            {
                Email = loginDto.Email,
                DisplayName = user.DisplayName,
                Token = _CreateTokenAsync(user)
            };
        else
            throw new UnauthorizedException();
    }
    public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
    {
        // Mapping Register Dto => Application User
        var user = new ApplicationUser()
        {
            Email = registerDto.Email,
            DisplayName = registerDto.DisplayName,
            UserName = registerDto.Username,
            PhoneNumber = registerDto.PhoneNumber,
        };
        // Create User 
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (result.Succeeded)
            return new UserDto
            {
                Email = registerDto.Email,
                DisplayName = user.DisplayName,
                Token = _CreateTokenAsync(user)
            };
        else
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            throw new BadRequestException(errors); 
        }
    }
    private static string _CreateTokenAsync(ApplicationUser user)
    {
        return "";
    }
}