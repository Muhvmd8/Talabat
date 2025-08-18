namespace Shared.DataTransferObjects.IdentityDTO;
public class UserDto
{
    [EmailAddress]
    public string Email { get; set; } = default!;
    public string Token { get; set; } = default!;
    public string DisplayName { get; set; } = default!;
}
