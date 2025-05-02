namespace FiltersDemo.Api.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is NotImplementedException)
        {
            context.Result = new ObjectResult("This feature is not implemented.")
            {
                StatusCode = StatusCodes.Status501NotImplemented   
            };
            
            context.ExceptionHandled = true;
        }

        base.OnException(context);
    }
}