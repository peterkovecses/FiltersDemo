namespace FiltersDemo.Api.Filters;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class LogResponseSizeAttribute : Attribute, IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var originalBody = context.HttpContext.Response.Body;

        using var memoryStream = new MemoryStream();
        context.HttpContext.Response.Body = memoryStream;

        await next();

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();
        var sizeInBytes = Encoding.UTF8.GetByteCount(responseBody);

        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<LogResponseSizeAttribute>>();
        logger.LogInformation("Response size for {Path}: {Size} bytes",
            context.HttpContext.Request.Path, sizeInBytes);

        memoryStream.Seek(0, SeekOrigin.Begin);
        await memoryStream.CopyToAsync(originalBody);
        context.HttpContext.Response.Body = originalBody;
    }
}