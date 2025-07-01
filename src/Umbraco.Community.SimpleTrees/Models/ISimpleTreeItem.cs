namespace Umbraco.Community.SimpleTrees.Models;

public interface ISimpleTreeItem
{
    public bool HasChildren { get; set; }
    public string Name { get; set; }
    public bool IsFolder { get; set; }
    public string Icon { get; set; }
    public string EntityType { get; set; }
    public string Unique { get; set; }
    public ISimpleTreeItemParent? Parent { get; set; }
}