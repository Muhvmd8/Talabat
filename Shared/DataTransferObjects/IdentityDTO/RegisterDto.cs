namespace Shared.DataTransferObjects.IdentityDTO;
public class RegisterDto
{
    [EmailAddress]
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
    [Phone]
    public string PhoneNumber { get; set; } = default!;
}
