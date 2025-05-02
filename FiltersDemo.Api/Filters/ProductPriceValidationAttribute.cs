namespace FiltersDemo.Api.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class ProductPriceValidationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments["request"] is not CreateProductRequest { Price: > 0 })
        {
            context.Result = new BadRequestObjectResult("The price of the product must be greater than zero.");
        }
    }
}