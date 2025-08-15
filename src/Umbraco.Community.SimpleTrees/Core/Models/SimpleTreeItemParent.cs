namespace Umbraco.Community.SimpleTrees.Core.Models;

public class SimpleTreeItemParent(string unique, string entityType) : ISimpleTreeItemParent
{
    public string EntityType { get; set; } = entityType;
    public string Unique { get; set; } = unique;

    public static ISimpleTreeItemParent? Create(string unique, string entityType)
    {
        return new SimpleTreeItemParent(unique, entityType);
    }
}