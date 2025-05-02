namespace FiltersDemo.Api.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    private const string ApiKeyHeaderName = "X-Api-Key";
    private const string ApiKey = "DEMO-API-KEY-2024";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var request = context.HttpContext.Request;

        if (!request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey) || extractedApiKey != ApiKey)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}