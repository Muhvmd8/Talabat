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
        //httphttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var statusCode = httpContext.Response.StatusCode = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError // Default
        };
        // Set content type as a json
        httpContext.Response.ContentType = "application/json";
        // Response object 
        var response = new ErrorResponse
        {
            StatusCode = statusCode,
            ErrorMessage = ex.Message
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
}
