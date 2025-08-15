namespace Umbraco.Community.SimpleTrees.Web.Models;

public class SimpleEntityActionExecuteResponse
{
    public required string Title { get; init; }
    public bool IsSuccess { get; init; }
    public required string Message { get; init; }

    public static SimpleEntityActionExecuteResponse Error(string title, string message)
    {
        return new SimpleEntityActionExecuteResponse
        {
            Title = title,
            Message = message,
            IsSuccess = false
        };
    }

    public static SimpleEntityActionExecuteResponse Success(string title, string message)
    {
        return new SimpleEntityActionExecuteResponse
        {
            Title = title,
            Message = message,
            IsSuccess = true
        };
    }
}