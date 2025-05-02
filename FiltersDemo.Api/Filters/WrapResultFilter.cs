namespace FiltersDemo.Api.Filters;

public class WrapResultFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult)
        {
            if (objectResult.Value is ApiResponse)
            {
                await next();
                
                return;
            }

            var statusCode = objectResult.StatusCode ?? StatusCodes.Status200OK;

            var response = new ApiResponse
            {
                Success = statusCode is >= 200 and < 300
            };

            if (response.Success)
            {
                response.Data = objectResult.Value;
            }
            else
            {
                response.Errors = objectResult.Value switch
                {
                    ValidationProblemDetails validationDetails => 
                        validationDetails.Errors.SelectMany(e => e.Value).ToList(),
                    ProblemDetails problemDetails =>
                        [problemDetails.Title ?? problemDetails.Detail ?? "An error occurred."],
                    string s => [s],
                    _ => [objectResult.Value?.ToString() ?? "An error occurred."]
                };
            }

            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }

        await next();
    }
}