namespace Presentation.Controllers;
[ApiController]
[Route("api/[Controller]")]
public abstract class ApiController : ControllerBase
{
    protected string GetEmailFromToken() => User.FindFirstValue(ClaimTypes.Email)!;
}
