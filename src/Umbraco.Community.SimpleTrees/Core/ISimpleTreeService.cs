using Umbraco.Cms.Core.Models;
using Umbraco.Community.SimpleTrees.Core.Models;

namespace Umbraco.Community.SimpleTrees.Core;

public interface ISimpleTreeService
{
    Task<PagedModel<ISimpleTreeItem>> GetTreeRootAsync(string treeAlias, int skip, int take, bool foldersOnly);
    Task<TargetPagedModel<ISimpleTreeItem>> GetTreeRootAsync(string treeAlias, string? unique, string entityType, int takeBefore, int takeAfter, bool foldersOnly);

    Task<TargetPagedModel<ISimpleTreeItem>> GetPagedTargetTreeChildrenAsync(
        string treeAlias,
        string targetUnique,
        string targetEntityType,
        string entityType,
        string parentUnique,
        int takeBefore,
        int takeAfter,
        bool foldersOnly);

    Task<PagedModel<ISimpleTreeItem>> GetPagedOffsetTreeChildrenAsync(string treeAlias, string entityType, string parentUnique, int skip, int take, bool foldersOnly);
    IEnumerable<ISimpleTree> GetAll();
    ISimpleTree? GetByAlias(string alias);
}