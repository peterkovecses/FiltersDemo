namespace FiltersDemo.Api.Filters;

public class NotImplExceptionFilter(ILogger<NotImplExceptionFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not NotImplementedException) return;
        logger.LogWarning("The called function is not implemented: {Message}", context.Exception.Message);

        context.Result = new ObjectResult("This feature is not implemented.")
        {
            StatusCode = StatusCodes.Status501NotImplemented
        };

        context.ExceptionHandled = true;
    }
}