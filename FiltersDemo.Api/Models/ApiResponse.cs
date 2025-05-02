namespace FiltersDemo.Api.Models;

public class ApiResponse
{
    public bool Success { get; set; } = true;
    public object? Data { get; set; }
    public List<string>? Errors { get; set; }
}