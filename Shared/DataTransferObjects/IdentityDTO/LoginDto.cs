namespace Shared.DataTransferObjects.IdentityDTO;
public class LoginDto
{
    [EmailAddress]
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
