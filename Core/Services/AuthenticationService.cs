namespace Services;
public class AuthenticationService(
    UserManager<ApplicationUser> _userManager,
    IConfiguration configuration, 
    IMapper mapper)
    : IAuthenticationService
{
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
                Token = await _CreateTokenAsync(user)
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
                Token = await _CreateTokenAsync(user)
            };
        else
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            throw new BadRequestException(errors); 
        }
    }
    public async Task<bool> CheckEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user is not null;  
    }
    public async Task<UserDto> GetCurrentUserAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email)?? 
            throw new UserNotFoundException(email);

        return new UserDto
        {
            Email = email,
            DisplayName = user.DisplayName,
            Token = await _CreateTokenAsync(user)
        };
    }
    public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
    {
        var user = await _userManager.Users.Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.Email == email) ?? 
                throw new UserNotFoundException(email);

        return user.Address is not null?  
            new AddressDto
            {
                FirstName =  user.Address.FirstName,
                LastName = user.Address.LastName,
                City = user.Address.City,
                Street = user.Address.Street,
                Country = user.Address.Country, 
            } : throw new AddressNotFoundException(user.Email!);
    }
    public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
    {
        var user = await _userManager.Users.Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.Email == email)??
            throw new AddressNotFoundException(email);

        // Update
        if (user.Address is not null)
        {
            user.Address.FirstName = addressDto.FirstName;
            user.Address.LastName = addressDto.LastName;
            user.Address.City = addressDto.City;
            user.Address.Street = addressDto.Street;
            user.Address.Country = addressDto.Country;
        }
        else // Add New 
            user.Address = mapper.Map<AddressDto, Address>(addressDto);

        await _userManager.UpdateAsync(user); // it will update the changes on the user and its composed properties 

        return mapper.Map<AddressDto>(user.Address);
    }
    private async Task<string> _CreateTokenAsync(ApplicationUser user)
    {
        // 1. Claims
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id!)
        };
        // 2. Add roles to the claims
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        // 3. Secret Key
        var secretKey = configuration["JWTOptions:SecretKey"];
        // Convert string to bytes
        var key =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)); 
        // 4. Identify credentials
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        // 5. Create token
        // Note:- the issuer, audience, and secret key are changable based on environment, so i will store them into app settings
        var token = new JwtSecurityToken
        (
            issuer: configuration["JWTOptions:Issuer"],
            audience: configuration["JWTOptions:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        // 6. Create object from JwtSecurityTokenHandler and call WriteToken()
        // to convert the token object to string.
        return new JwtSecurityTokenHandler().WriteToken(token); 
    }
}