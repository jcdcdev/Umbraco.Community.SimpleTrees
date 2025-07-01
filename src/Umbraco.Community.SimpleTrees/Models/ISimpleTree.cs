using Umbraco.Cms.Core.Models;

namespace Umbraco.Community.SimpleTrees.Models;

public interface ISimpleTree
{
    string Alias { get; }
    int Weight { get; }
    string Name { get; }
    string Label { get; }
    string[] Menus { get; }
    string DefaultEntityType { get; }
    string DefaultRootEntityType { get; }
    bool HasAlias(string treeAlias);
    Task<PagedModel<ISimpleTreeItem>> GetTreeRootAsync(int skip, int take, bool foldersOnly);
    Task<PagedModel<ISimpleTreeItem>> GetTreeChildrenAsync(string entityType, string parentUnique, int skip, int take, bool foldersOnly);
}