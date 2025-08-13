namespace Umbraco.Community.SimpleTrees.Web.Models;

public class SimpleTreeItemResponse
{
    public required string Name { get; set; }
    public required string Icon { get; set; }
    public required string EntityType { get; set; }
    public required string Unique { get; set; }
    public bool HasChildren { get; set; }
    public bool IsFolder { get; set; }
    public SimpleTreeItemParentResponse? Parent { get; set; }
}