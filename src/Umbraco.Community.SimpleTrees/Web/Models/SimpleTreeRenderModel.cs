namespace Umbraco.Community.SimpleTrees.Web.Models;

public class SimpleTreeRenderModel
{
    public required string Body { get; set; }
    public static SimpleTreeRenderModel Error => new() { Body = Constants.ErrorView };

    public static SimpleTreeRenderModel Create(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            throw new ArgumentException("Body cannot be null or whitespace.", nameof(body));
        }

        return new SimpleTreeRenderModel { Body = body };
    }
}