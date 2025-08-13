namespace Umbraco.Community.SimpleTrees.Web.Models;

public class SimpleEntityActionRequest
{
    public required string Unique { get; set; }
    public required string EntityType { get; set; }
    public required string ActionAlias { get; set; }
}