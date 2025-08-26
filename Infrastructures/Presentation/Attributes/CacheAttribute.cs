namespace Presentation.Attributes;
class CacheAttribute(int duration=90) : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        string cacheKey = _GenerateCacheKey(context.HttpContext.Request);

        ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
        var cacheValue = await cacheService.GetAsync(cacheKey);

        if (cacheValue is not null)
        {
            context.Result = new ContentResult
            {
                Content = cacheValue.ToString(),
                ContentType = "application/json",
                StatusCode = 200
            };
            return;
        }

        var executedContext = await next();
        if (executedContext.Result is OkObjectResult result)
        {
           await cacheService.SetAsync(cacheKey, result.Value, TimeSpan.FromSeconds(duration));
        }
    }

    private string _GenerateCacheKey(HttpRequest request)
    {
        StringBuilder key = new();
        key.Append(request.Path + '?');

        foreach (var item in request.Query.OrderBy(q => q.Key))
            key.Append($"{item.Key}={item.Value}&");

        return key.ToString();
    }
}