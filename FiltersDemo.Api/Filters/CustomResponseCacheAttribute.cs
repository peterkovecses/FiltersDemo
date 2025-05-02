namespace FiltersDemo.Api.Filters;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomResponseCacheAttribute(int durationInSeconds) : Attribute, IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var cache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
        var cacheKey = GenerateCacheKey(context.HttpContext.Request);

        if (cache.TryGetValue(cacheKey, out string? cachedResponse))
        {
            context.Result = new ContentResult
            {
                Content = cachedResponse,
                ContentType = "application/json",
                StatusCode = 200
            };
            
            return;
        }

        var executedContext = await next();

        if (executedContext.Result is ObjectResult objectResult)
        {
            var responseContent = JsonSerializer.Serialize(objectResult.Value);
            cache.Set(cacheKey, responseContent, TimeSpan.FromSeconds(durationInSeconds));
        }
    }

    private static string GenerateCacheKey(HttpRequest request) 
        => $"{request.Path}{request.QueryString}";
}