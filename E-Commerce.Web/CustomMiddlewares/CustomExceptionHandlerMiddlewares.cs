namespace E_Commerce.Web.CustomMiddlewares;
public class CustomExceptionHandlerMiddlewares
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddlewares> _logger;
    public CustomExceptionHandlerMiddlewares(RequestDelegate next, ILogger<CustomExceptionHandlerMiddlewares> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
            await _HandleNotFoundEndPointAsync(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            await _HandleExceptionAsync(httpContext, ex);
        }
    }
    private static async Task _HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        // Set status code for the response.
        // Set content type as a json
        //httpContext.Response.ContentType = "application/json";
        // Response object 
        var response = new ErrorResponse
        {
            ErrorMessage = ex.Message
        };
        //httphttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        response.StatusCode = httpContext.Response.StatusCode = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            BadRequestException badRequestException => _GetBadRequestErrors(badRequestException, response),
            _ => StatusCodes.Status500InternalServerError // Default
        };
        // Return object as JSON
        await httpContext.Response.WriteAsJsonAsync(response); // Selialize => Write
    }
    private static async Task _HandleNotFoundEndPointAsync(HttpContext httpContext)
    {
        if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
        {
            var respone = new ErrorResponse
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"End point {httpContext.Request.Path} is not found!"
            };

            await httpContext.Response.WriteAsJsonAsync(respone);
        }
    }
    private static int _GetBadRequestErrors(BadRequestException badRequestException, ErrorResponse response)
    {
        response.Errors = badRequestException.Errors;
        return StatusCodes.Status400BadRequest;
    }
}