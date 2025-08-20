namespace Presentation.Controllers;
public class AuthenticationController(IServiceManager serviceManager)
    : ApiController
{
    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await serviceManager.AuthenticationService.LoginAsync(loginDto);
        return Ok(user);
    }
    [HttpPost("Register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var user = await serviceManager.AuthenticationService.RegisterAsync(registerDto);
        return Ok(user);
    }
    [Authorize]
    [HttpGet("CurrentUser")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await serviceManager.AuthenticationService.GetCurrentUserAsync(email!);
        return Ok(user);
    }
    [Authorize]
    [HttpGet("UserAddress")]
    public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var address = await serviceManager.AuthenticationService.GetCurrentUserAddressAsync(email!);
        return Ok(address);
    }
    [Authorize]
    [HttpPost("UpdateUserAddress")]
    public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var address = await serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(email!, addressDto);
        return Ok(address);
    }
    [HttpGet("CheckEmail")]
    public async Task<ActionResult<bool>> CheckEmail([FromQuery] string email)
    {
        var result = await serviceManager.AuthenticationService.CheckEmailAsync(email);
        return Ok(result);
    }
}