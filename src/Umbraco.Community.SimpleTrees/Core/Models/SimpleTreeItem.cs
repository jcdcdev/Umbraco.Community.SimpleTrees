namespace Umbraco.Community.SimpleTrees.Core.Models;

public class SimpleTreeItem(string name, string unique, string entityType, string icon) : ISimpleTreeItem
{
    public bool HasChildren { get; set; }
    public string Name { get; set; } = name;
    public bool IsFolder { get; set; }
    public string Icon { get; set; } = icon;
    public string EntityType { get; set; } = entityType;
    public string Unique { get; set; } = unique;
    public ISimpleTreeItemParent? Parent { get; set; }

    public static ISimpleTreeItem Create(
        string name,
        string unique,
        string entityType,
        string icon = "icon-document",
        bool isFolder = false,
        bool hasChildren = false,
        ISimpleTreeItemParent? parent = null)
    {
        var item = new SimpleTreeItem(name, unique, entityType, icon)
        {
            IsFolder = isFolder,
            HasChildren = hasChildren,
            Parent = parent
        };

        return item;
    }
}