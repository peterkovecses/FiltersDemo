namespace FiltersDemo.Api.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class NotFoundIfNullAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult { Value: null })
        {
            context.Result = new NotFoundResult();
        }
        
        base.OnActionExecuted(context);
    }
}