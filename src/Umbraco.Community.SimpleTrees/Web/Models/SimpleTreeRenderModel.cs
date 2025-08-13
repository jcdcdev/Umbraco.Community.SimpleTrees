namespace Umbraco.Community.SimpleTrees.Web.Models;

public class SimpleTreeRenderModel
{
    public required string Body { get; set; }
    public static SimpleTreeRenderModel Error => new() { Body = Constants.ErrorView };
    public static SimpleTreeRenderModel Create(string body) => new() { Body = body };
}